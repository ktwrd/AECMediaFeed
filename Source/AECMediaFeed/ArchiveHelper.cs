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

public static class ArchiveHelper
{

    public delegate bool GetStreamDictionaryPredicate(
        ZipArchiveEntry entry,
        string key);

    public delegate bool GetStreamDictionaryFullNamePredicate(
        string entryFullName,
        string key);

    public static async Task<IReadOnlyDictionary<string, MemoryStream>> GetStreamDictionary(
        ZipArchive archive,
        GetStreamDictionaryPredicate predicate,
        params string[] filenames)
    {
        var result = new Dictionary<string, MemoryStream>();
        foreach (var filename in filenames)
        {
            var entry = archive.Entries.FirstOrDefault(e => predicate(e, filename));
            if (entry != null)
            {
                using var entryStream = entry.Open();
                var ms = new MemoryStream();
                await entryStream.CopyToAsync(ms);
                result[filename] = ms;
            }
        }
        return result;
    }
    public static async Task<IReadOnlyDictionary<string, MemoryStream>> GetStreamDictionary(
        ZipArchive archive,
        GetStreamDictionaryFullNamePredicate predicate,
        params string[] filenames)
    {
        var result = new Dictionary<string, MemoryStream>();
        foreach (var filename in filenames)
        {
            var entry = archive.Entries
                .Select(e => new
                {
                    Entry = e,
                    FullName = e.FullName.Replace('\\', '/')
                })
                .FirstOrDefault(e => predicate(e.FullName, filename));
            if (entry?.Entry != null)
            {
                using var entryStream = entry.Entry.Open();
                var ms = new MemoryStream();
                await entryStream.CopyToAsync(ms);
                result[filename] = ms;
            }
        }
        return result;
    }
}
