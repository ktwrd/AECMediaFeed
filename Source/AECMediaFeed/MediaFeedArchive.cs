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

using System.IO.Compression;

namespace AECMediaFeed;

/// <summary>
/// Abstraction layer for accessing zip archives from the AEC Media Feed
/// </summary>
public class MediaFeedArchive : IDisposable
{
    public void Dispose()
    {
        if (_disposeArchive)
        {
            _archive.Dispose();
        }
        GC.SuppressFinalize(this);
    }
    public MediaFeedArchive(ZipArchive archive, bool disposeArchive = true)
    {
        _disposeArchive = disposeArchive;
        _archive = archive;

        _filenames = _archive.Entries
            .Select(e => e.FullName.Replace('\\', '/'))
            .Distinct().ToArray();
    }
    private readonly bool _disposeArchive;
    private readonly ZipArchive _archive;
    private readonly string[] _filenames;

    public IReadOnlyList<string> Filenames => _filenames;

    /// <summary>
    /// Get the stream of a specific archive feature.
    /// </summary>
    /// <param name="feature">Must only have one flag (<see cref="MediaFeedArchiveFeatures.None"/> is ignored)</param>
    /// <returns>Stream of the archive entry from <see cref="ZipArchiveEntry.Open"/></returns>
    /// <exception cref="ArgumentException">Thrown when there are more than one (or no) flags in <paramref name="feature"/></exception>
    /// <exception cref="NotImplementedException">Thrown when the index of <see cref="EnumFilenamesMapping"/> is <c>-1</c> when using the single flag in <paramref name="feature"/></exception>
    /// <exception cref="InvalidOperationException">Thrown when the archive entry could not be found.</exception>
    /// <inheritdoc cref="GetEntry(MediaFeedArchiveFeatures, out string?)"/>
    public Stream GetStream(MediaFeedArchiveFeatures feature)
    {
        var entry = GetEntry(feature, out var filename);
        if (entry == null)
        {
            throw new InvalidOperationException($"Could not find file \"{filename}\" in archive for feature {feature}");
        }
        return entry.Open();
    }

    /// <inheritdoc cref="GetEntry(MediaFeedArchiveFeatures, out string?)"/>
    public ZipArchiveEntry? GetEntry(MediaFeedArchiveFeatures feature)
        => GetEntry(feature, out var _);

    /// <summary>
    /// Get the <see cref="ZipArchiveEntry"/> for a specific archive feature.
    /// </summary>
    /// <param name="feature">Must only have one flag (<see cref="MediaFeedArchiveFeatures.None"/> is ignored)</param>
    /// <param name="filename">Filename that was used when fetching/finding the file in the zip archive.</param>
    /// <returns><see cref="ZipArchiveEntry"/> for the single flag in <paramref name="feature"/></returns>
    /// <exception cref="ArgumentException">Thrown when there are more than one (or no) flags in <paramref name="feature"/></exception>
    /// <exception cref="NotImplementedException">Thrown when the index of <see cref="EnumFilenamesMapping"/> is <c>-1</c> when using the single flag in <paramref name="feature"/></exception>
    public ZipArchiveEntry? GetEntry(MediaFeedArchiveFeatures feature, out string? filename)
    {
        filename = null;
        var electionId = FindElectionId();
        var featureFlags = feature.GetFlags(ignoreZero: true);
        if (featureFlags.Length < 1)
        {
            throw new ArgumentException("No flags provided.", nameof(feature));
        }
        if (featureFlags.Length > 1)
        {
            throw new ArgumentException("Only one flag is allowed!", nameof(feature));
        }
        var index = EnumFilenamesMapping.IndexOf(featureFlags[0]);
        if (index == -1)
        {
            throw new NotImplementedException($"File mapping not implemented for Archive Feature \"{featureFlags[0]}\"");
        }
        filename = $"xml/{EnumFilenames[index]}-{electionId}.xml";
        var filenameL = filename;
        return _archive.Entries
            .FirstOrDefault(e
                => e.FullName.Replace('\\', '/')
                             .Equals(filenameL, StringComparison.OrdinalIgnoreCase));
    }

    private static readonly string[] EnumFilenames = [
            "eml-110-event",
            "eml-230-candidates",

            "eml-510-count",
            "eml-520-results",

            "aec-mediafeed-pollingdistricts",

            "aec-mediafeed-results-standard-light",
            "aec-mediafeed-results-standard-preload",
            "aec-mediafeed-results-standard-verbose",

            "aec-mediafeed-results-detailed-light",
            "aec-mediafeed-results-detailed-lightprogress",
            "aec-mediafeed-results-detailed-preload",
            "aec-mediafeed-results-detailed-verbose"
        ];
    private static readonly List<MediaFeedArchiveFeatures> EnumFilenamesMapping = [
            MediaFeedArchiveFeatures.EMLEvent110,
            MediaFeedArchiveFeatures.EMLCandidates230,

            MediaFeedArchiveFeatures.EMLCount510,
            MediaFeedArchiveFeatures.EMLResult520,

            MediaFeedArchiveFeatures.AECPollingDistricts,

            MediaFeedArchiveFeatures.AECResultStandardLight,
            MediaFeedArchiveFeatures.AECResultStandardPreload,
            MediaFeedArchiveFeatures.AECResultStandardVerbose,

            MediaFeedArchiveFeatures.AECResultDetailedLight,
            MediaFeedArchiveFeatures.AECResultDetailedLightProgress,
            MediaFeedArchiveFeatures.AECResultDetailedPreload,
            MediaFeedArchiveFeatures.AECResultDetailedVerbose
        ];

    public MediaFeedArchiveFeatures GetFeatures()
    {
        var electionId = FindElectionId();
        var flags = MediaFeedArchiveFeatures.None;
        for (int i = 0; i < EnumFilenames.Length; i++)
        {
            if (_filenames.Any(e => e.Equals($"xml/{EnumFilenames[i]}-{electionId}.xml", StringComparison.OrdinalIgnoreCase)))
            {
                flags |= EnumFilenamesMapping[i];
            }
        }
        return flags;
    }
    private string FindElectionId()
    {
        var x = _filenames.Where(e => e.StartsWith("xml/", StringComparison.OrdinalIgnoreCase) && e.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            .Select(e => e.Split("/")[^1].Split('-')[^1].Split('.')[0].Trim())
            .FirstOrDefault(e => long.TryParse(e, out var _) && !string.IsNullOrEmpty(e));
        if (x == null)
        {
            throw new InvalidOperationException(
                $"Could not figure out Election ID from any of the following files:{Environment.NewLine}"
                + string.Join(Environment.NewLine, _filenames));
        }
        return x;
    }
}

[Flags]
public enum MediaFeedArchiveFeatures
{
    None = 0,

    EMLEvent110 = 1 << 0,
    EMLCandidates230 = 1 << 1,
    EMLCount510 = 1 << 2,
    EMLResult520 = 1 << 4,

    AECPollingDistricts = 1 << 16,
    AECResultStandardLight = 1 << 17,
    AECResultStandardPreload = 1 << 18,
    AECResultStandardVerbose = 1 << 19,

    AECResultDetailedLight = 1 << 22,
    AECResultDetailedLightProgress = 1 << 23,
    AECResultDetailedPreload = 1 << 24,
    AECResultDetailedVerbose = 1 << 25,
}
