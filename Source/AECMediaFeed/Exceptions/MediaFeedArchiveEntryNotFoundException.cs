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

using FluentFTP;
using System.IO.Compression;

namespace AECMediaFeed;

/// <remarks>
/// Generated with
/// <see href="https://ktwrd.github.io/csharp-exception-generator.html"/>
/// </remarks>
public class MediaFeedArchiveEntryNotFoundException : Exception
{
    #region Constructors
    public MediaFeedArchiveEntryNotFoundException(
        FtpListItem file,
        ZipArchive archive,
        string targetEntryFilename)
        : base()
    {
        TargetEntryFilename = targetEntryFilename;
        ArchiveFilenames = archive.Entries.Select(e => e.FullName).ToArray();
        File = file;
    }
    #endregion

    public override string ToString()
    {
        var lines = new[]
        {
            $"Could not find file \"{TargetEntryFilename}\" in remote file: {File.FullName}",
            $"-- Start Archive Entries ({ArchiveFilenames.Count}) --"
        }.Concat(ArchiveFilenames).Concat([
            "-- End Archive Entries --",
            base.ToString()
        ]);
        return string.Join(Environment.NewLine, lines);
    }

    public string TargetEntryFilename { get; private set; }
    public IReadOnlyList<string> ArchiveFilenames { get; private set; }
    public FtpListItem File { get; private set; }
}
