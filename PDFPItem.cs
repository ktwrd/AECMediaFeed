﻿/*
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

namespace AECMediaFeed;

public class PDFPItem
{
    public string State { get; set; }
    public string Electorate { get; set; }
    public long VotesCounted { get; set; }
    public string VotesCountedPerc { get; set; }
    public int VotesCountedProgress { get; set; }
    public string PartyName { get; set; }
    public string PartyVotesPercent { get; set; }
    public long PartyVotes { get; set; }
}
