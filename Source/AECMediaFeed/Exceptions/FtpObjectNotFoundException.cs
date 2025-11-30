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

namespace AECMediaFeed;

/// <remarks>
/// Generated with
/// <see href="https://ktwrd.github.io/csharp-exception-generator.html"/>
/// </remarks>
public class FtpObjectNotFoundException : Exception
{
    #region Constructors
    public FtpObjectNotFoundException(string? message) : base(message)
    { }
    #endregion

    /// <inheritdoc/>
    public override string ToString()
    {
        return base.ToString();
    }

    /// <summary>
    /// FTP Client that was used.
    /// </summary>
    public FtpClient? Client { get; init; }

    /// <summary>
    /// Path that was used with <see cref="FtpClient.GetObjectInfo(string, bool)"/>
    /// </summary>
    public required string Path { get; init; }
}
