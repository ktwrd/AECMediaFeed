/*
   Copyright 2025 Kate Ward <kate@dariox.club>

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */

using CSharpFunctionalExtensions;
using FluentFTP;
using NLog;
using System.ComponentModel;
using System.IO.Compression;

namespace AECMediaFeed;

public class MediaFeedClient : INotifyPropertyChanged
{
    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }
    #endregion

    private readonly Logger _log = LogManager.GetCurrentClassLogger();

    #region Properties
    private bool _isConnected = false;
    private string _ftpClientHost = "mediafeedarchive.aec.gov.au";
    private SwitchHostKind _ftpClientHostKind = SwitchHostKind.Archive;
    private FtpClient? _ftpClient;
    private readonly SemaphoreSlim _ftpClientSemaphore = new SemaphoreSlim(1);

    public bool IsConnected
    {
        get => _isConnected;
        private set
        {
            _isConnected = value;
            RaisePropertyChanged(nameof(IsConnected));
        }
    }
    /// <summary>
    /// Defaults to <c>mediafeedarchive.aec.gov.au</c>
    /// </summary>
    public string FtpClientHost
    {
        get => _ftpClientHost;
        private set
        {
            _ftpClientHost = value;
            RaisePropertyChanged(nameof(FtpClientHost));
        }
    }
    /// <summary>
    /// FTP Host Kind. Use <see cref="SwitchHostAsync"/> to change this (along with using the live feed if it's available)
    /// </summary>
    public SwitchHostKind FtpClientHostKind
    {
        get => _ftpClientHostKind;
        private set
        {
            _ftpClientHostKind = value;
            RaisePropertyChanged(nameof(FtpClientHostKind));
        }
    }
    /// <summary>
    /// <b>PLEASE USE <see cref="FtpClientSemaphore"/> WHEN USING THIS!!!!!</b>
    /// </summary>
    internal FtpClient? FtpClient
    {
        get => _ftpClient;
        private set
        {
            _ftpClient = value;
            RaisePropertyChanged(nameof(FtpClient));
        }
    }
    /// <summary>
    /// PLEASE USE THIS WHEN ACCESSING <see cref="FtpClient"/>
    /// </summary>
    internal SemaphoreSlim FtpClientSemaphore => _ftpClientSemaphore;
    #endregion

    public async Task SwitchHostAsync(SwitchHostKind kind)
    {
        if (kind == FtpClientHostKind)
        {
            _log.Trace($"Not required. Already on {nameof(FtpClientHostKind)}={FtpClientHostKind}");
            return;
        }

        await _ftpClientSemaphore.WaitAsync();
        try
        {
            if (FtpClient != null && FtpClient.IsConnected)
            {
                FtpClient.Disconnect();
                FtpClient.Dispose();
            }
            FtpClient = null;
            IsConnected = false;
            FtpClientHostKind = kind;
            FtpClientHost = kind switch
            {
                SwitchHostKind.Archive => "mediafeedarchive.aec.gov.au",
                SwitchHostKind.Live => "mediafeed.aec.gov.au",
                _ => throw new NotImplementedException($"Unknown value {kind} for {nameof(FtpClientHostKind)}")
            };
            await ConnectAsyncInternal(true);
            _log.Trace($"Successfully switched to host: {FtpClientHost}");
        }
        finally
        {
            _ftpClientSemaphore.Release();
        }
    }

    public enum SwitchHostKind
    {
        /// <summary>
        /// <para><b>Only online during a federal election.</b></para>
        /// <c>ftp://mediafeed.aec.gov.au</c>
        /// </summary>
        Live,
        /// <summary>
        /// <para><b>Always online. Only has data of previous elections.</b></para>
        /// <c>ftp://mediafeedarchive.aec.gov.au</c>
        /// </summary>
        Archive
    }

    public async Task ConnectAsync(bool allowReconnect = false)
    {
        await _ftpClientSemaphore.WaitAsync();
        try
        {
            await ConnectAsyncInternal(allowReconnect);
        }
        finally
        {
            _ftpClientSemaphore.Release();
        }
    }

    private async Task ConnectAsyncInternal(bool allowReconnect = false)
    {
        if (FtpClient != null && IsConnected && FtpClient.IsConnected)
        {
            if (allowReconnect)
            {
                _log.Info("Disconnecting from AEC Media Feed");
                try
                {
                    FtpClient.Disconnect();
                    FtpClient.Dispose();
                    FtpClient = null;
                    IsConnected = false;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to disconnect from AEC FTP Server", ex);
                }
            }
            else
            {
                return;
            }
        }
        IsConnected = false;
        FtpClient = null;
        var client = new FtpClient(FtpClientHost);
        client.AutoConnect();
        FtpClient = client;
        IsConnected = true;
        _log.Info("Connected to the AEC!");
    }

    public async Task<string[]> GetElectionIdsAsync()
    {
        if (!IsConnected) await ConnectAsync();
        await FtpClientSemaphore.WaitAsync();
        string[] result;
        try
        {
            result = await GetElectionIdsAsyncInternal();
        }
        finally
        {
            FtpClientSemaphore.Release();
        }
        return result;
    }
    private async Task<string[]> GetElectionIdsAsyncInternal()
    {
        if (FtpClient == null) throw new InvalidOperationException($"{nameof(FtpClient)} is still null after connecting!");
        return FtpClient.GetNameListing("/")
            .Where(e => long.TryParse(e, out var _))
            .ToArray();
    }

    #region Standard Verbose
    public async Task<MemoryStream> GetStandardVerboseLatest(string electionId)
    {
        var archiveStream = await GetDataVerbose(
            electionId,
            MediaFeedDataGranularity.Standard,
            MediaFeedDataVerbosity.Verbose,
            when: GetDataWhen.Latest);
        return await FindStandardVerboseInternal(electionId, archiveStream);
    }
    public async Task<MemoryStream> GetStandardVerboseAt(string electionId, DateTime timestamp)
    {
        var archiveStream = await GetDataVerbose(
            electionId,
            MediaFeedDataGranularity.Standard,
            MediaFeedDataVerbosity.Verbose,
            when: GetDataWhen.Specific,
            whenSpecific: timestamp);
        return await FindStandardVerboseInternal(electionId, archiveStream);
    }
    private static async Task<MemoryStream> FindStandardVerboseInternal(
        string electionId,
        GetDataVerboseResult dataResult)
    {
        var archive = new ZipArchive(dataResult.Stream);
        var filename = $"xml/aec-mediafeed-results-standard-verbose-{electionId}.xml";
        var streams = await ArchiveHelper.GetStreamDictionary(
            archive,
            (fullname, key) => fullname.EndsWith(key, StringComparison.OrdinalIgnoreCase),
            filename);
        if (streams.TryGetValue(filename, out var stream))
        {
            return stream;
        }
        throw new MediaFeedArchiveEntryNotFoundException(dataResult.Item, archive, filename);
    }
    #endregion

    #region Standard EML
    public async Task<StandardEmlResult> GetStandardEmlLatest(string electionId)
    {
        var data = await GetDataVerbose(
            electionId,
            MediaFeedDataGranularity.Standard,
            MediaFeedDataVerbosity.Eml,
            when: GetDataWhen.Latest);
        return await FindStandardEmlInternal(electionId, data);
    }

    public async Task<StandardEmlResult> GetStandardEmlAt(string electionId, DateTime timestamp)
    {
        var data = await GetDataVerbose(
            electionId,
            MediaFeedDataGranularity.Standard,
            MediaFeedDataVerbosity.Eml,
            when: GetDataWhen.Specific,
            whenSpecific: timestamp);
        return await FindStandardEmlInternal(electionId, data);
    }

    
    private static async Task<StandardEmlResult> FindStandardEmlInternal(
        string electionId,
        GetDataVerboseResult ftpData)
    {
        var archive = new ZipArchive(ftpData.Stream);
        var filenames = new[]
        {
            $"xml/eml-510-count-{electionId}.zip",
            $"xml/eml-520-result-{electionId}.zip",
        };

        var streams = await ArchiveHelper.GetStreamDictionary(
            archive,
            (fullname, key) => fullname.EndsWith(key, StringComparison.OrdinalIgnoreCase),
            filenames);


        Maybe<MemoryStream> stream510 = Maybe<MemoryStream>.None;
        Maybe<MemoryStream> stream520 = Maybe<MemoryStream>.None;

        if (streams.TryGetValue(filenames[0], out var stream510d))
        {
            stream510 = stream510d;
        }
        if (streams.TryGetValue(filenames[1], out var stream520d))
        {
            stream520 = stream520d;
        }

        if (stream510.HasNoValue && stream520.HasNoValue)
        {
            throw new AggregateException("Failed to find any valid EML file in archive: " + ftpData.Item.FullName,
                new MediaFeedArchiveEntryNotFoundException(ftpData.Item, archive, filenames[0]),
                new MediaFeedArchiveEntryNotFoundException(ftpData.Item, archive, filenames[1]));
        }

        return new StandardEmlResult
        {
            CountStream = stream510,
            ResultStream = stream520
        };
    }

    public class StandardEmlResult
    {
        /// <summary>
        /// Memory Stream containing XML that has schema akin to <see cref="Schema."/>
        /// </summary>
        public Maybe<MemoryStream> CountStream { get; set; }
        public Maybe<MemoryStream> ResultStream { get; set; }

        public Schema.EML_510Count.EML GetCount()
        {
            if (CountStream.HasNoValue)
            {
                throw new InvalidOperationException(nameof(CountStream) + " is missing.");
            }

            return CountStream.Value.ParseXml<Schema.EML_510Count.EML>();
        }
        public Schema.EML_520Result.EML GetResult()
        {
            if (CountStream.HasNoValue)
            {
                throw new InvalidOperationException(nameof(CountStream) + " is missing.");
            }

            return CountStream.Value.ParseXml<Schema.EML_520Result.EML>();
        }
    }
    #endregion

    public async Task<Stream> GetData(
        string electionId,
        MediaFeedDataGranularity granularity,
        MediaFeedDataVerbosity verbosity,
        GetDataWhen when = GetDataWhen.Latest,
        DateTime? whenSpecific = null)
    {
        var result = await GetDataVerbose(
            electionId,
            granularity,
            verbosity,
            when,
            whenSpecific);
        return result.Stream;
    }
    public async Task<GetDataVerboseResult> GetDataVerbose(
        string electionId,
        MediaFeedDataGranularity granularity,
        MediaFeedDataVerbosity verbosity,
        GetDataWhen when = GetDataWhen.Latest,
        DateTime? whenSpecific = null)
    {
        GetDataVerboseResult result;
        try
        {
            result = await GetDataVerboseInternal(
                electionId,
                granularity,
                verbosity,
                when,
                whenSpecific);
        }
        finally
        {
            FtpClientSemaphore.Release();
        }
        return result;

    }
    private async Task<GetDataVerboseResult> GetDataVerboseInternal(
        string electionId,
        MediaFeedDataGranularity granularity,
        MediaFeedDataVerbosity verbosity,
        GetDataWhen when = GetDataWhen.Latest,
        DateTime? whenSpecific = null)
    {
        await ConnectAsyncInternal();
        if (FtpClient == null) throw new InvalidOperationException($"{nameof(FtpClient)} is still null after connecting!");

        const string folderFmt = "/{0}/{1}/{2}";
        const string specificFilenameFmt = "aec-mediafeed-{1}-{2}-{0}-{3}.zip";

        var folder = string.Format(folderFmt, electionId, granularity, verbosity);

        FtpListItem? file = null;
        if (when == GetDataWhen.Specific)
        {
            if (whenSpecific == null)
            {
                throw new ArgumentNullException(
                    nameof(whenSpecific),
                    $"Required when argument {nameof(when)} is {GetDataWhen.Specific}");
            }

            var filename = string.Format(specificFilenameFmt,
                electionId, granularity, verbosity,
                whenSpecific.Value.ToString("yyyyMMddHHmmss"));
            file = FtpClient.GetObjectInfo($"{folder}/{filename}", dateModified: false);
            if (file == null)
            {
                throw new FtpObjectNotFoundException(
                    $"Could not find object: {filename}")
                {
                    Client = FtpClient,
                    Path = filename
                };
            }
        }
        else if (when == GetDataWhen.Latest || when == GetDataWhen.Oldest)
        {
            var listing = FtpClient.GetListing(folder);
            if (listing == null)
            {
                throw new FtpObjectNotFoundException(
                    $"Could not get listing for object: {folder}")
                {
                    Client = FtpClient,
                    Path = folder
                };
            }

            if (listing.Length == 0)
            {
                throw new FtpEmptyFolderException("Folder is empty: " + folder)
                {
                    Client = FtpClient,
                    Path = folder
                };
            }

            file = when switch
            {
                GetDataWhen.Latest => listing.OrderByDescending(e => e.Name).First(),
                GetDataWhen.Oldest => listing.OrderBy(e => e.Name).First(),
                _ => throw new NotImplementedException($"This shouldn't happen, but {nameof(when)} is {when}")
            };
        }
        else
        {
            throw new NotImplementedException($"Where parameter {nameof(when)} is {when}");
        }

        if (file == null)
        {
            throw new InvalidOperationException("Could not find file suitable for the provided parameters");
        }

        using var stream = FtpClient.OpenRead(file.FullName);
        var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        ms.Seek(0, SeekOrigin.Begin);
        return new GetDataVerboseResult(ms, file);
    }

    public class GetDataVerboseResult(Stream stream, FtpListItem item)
    {
        public Stream Stream { get; } = stream;
        public FtpListItem Item { get; } = item;
    }


    public enum GetDataWhen
    {
        Latest,
        Oldest,
        Specific
    }

    public enum MediaFeedDataGranularity
    {
        Standard,
        /// <summary>
        /// Only use Detailed if you want data down to the polling place. Should only be polled occasionally
        /// </summary>
        Detailed
    }

    public enum MediaFeedDataVerbosity
    {
        Eml,
        /// <summary>
        /// Only contains the election "results" XML data from verbose, and doesn't contain the schema.
        /// </summary>
        Light,
        /// <summary>
        /// Contains the schema, polling districts, preload results xml, EML 110 (Event), and EML 230 (Candidates).
        /// </summary>
        Preload,
        /// <summary>
        /// Contains the schema, and the verbose results xml.
        /// </summary>
        Verbose
    }
}
