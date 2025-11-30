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

using System.Xml.Serialization;

namespace AECMediaFeed.Schema;

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://www.aec.gov.au/xml/schema/mediafeed")]
[XmlRoot(Namespace = "http://www.aec.gov.au/xml/schema/mediafeed", IsNullable = false)]
public class MediaFeedRootElement
{
    public AEC_Results.EventResultsStructure? Results { get; set; }
    public AEC_PollingDistricts.PollingDistrictListStructure? PollingDistrictList { get; set; }
    public AEC_GroupVotingTickets.SenateGroupVotingTicketsStructure? SenateGroupVotingTickets { get; set; }
    public AEC_BallotPapers.BallotPapers? BallotPapers { get; set; }
}
