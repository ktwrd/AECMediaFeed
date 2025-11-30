@echo off

xsd /namespace:AECMediaFeed.Schema.AEC_BallotPapers EML\external\emltimestamp.xsd EML\external\xmldsig-core-schema.xsd EML\external\xAL.xsd EML\external\xNL.xsd EML\emlcore-v5-0.xsd AEC\aec-mediafeed-core-v3-0.xsd AEC\aec-mediafeed-ballotpapers-v3-0.xsd /classes
xsd /namespace:AECMediaFeed.Schema.AEC_GroupVotingTickets EML\external\emltimestamp.xsd EML\external\xmldsig-core-schema.xsd EML\external\xAL.xsd EML\external\xNL.xsd EML\emlcore-v5-0.xsd AEC\aec-mediafeed-core-v3-0.xsd AEC\aec-mediafeed-groupvotingtickets-v3-0.xsd /classes
xsd /namespace:AECMediaFeed.Schema.AEC_PollingDistricts EML\external\emltimestamp.xsd EML\external\xmldsig-core-schema.xsd EML\external\xAL.xsd EML\external\xNL.xsd EML\emlcore-v5-0.xsd AEC\aec-mediafeed-core-v3-0.xsd AEC\aec-mediafeed-pollingdistricts-v3-0.xsd /classes
xsd /namespace:AECMediaFeed.Schema.AEC_Results EML\external\emltimestamp.xsd EML\external\xmldsig-core-schema.xsd EML\external\xAL.xsd EML\external\xNL.xsd EML\emlcore-v5-0.xsd AEC\aec-mediafeed-core-v3-0.xsd AEC\aec-mediafeed-results-v3-0.xsd /classes

xsd /namespace:AECMediaFeed.Schema.EML_510Count EML\external\emltimestamp.xsd EML\external\xmldsig-core-schema.xsd EML\external\xAL.xsd EML\external\xNL.xsd EML\emlcore-v5-0.xsd EML\510-count-v5-0.xsd /classes
xsd /namespace:AECMediaFeed.Schema.EML_520Result EML\external\emltimestamp.xsd EML\external\xmldsig-core-schema.xsd EML\external\xAL.xsd EML\external\xNL.xsd EML\emlcore-v5-0.xsd EML\520-result-v5-0.xsd /classes
