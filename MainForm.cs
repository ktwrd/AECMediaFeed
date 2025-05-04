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
using FluentFTP;
using System.Diagnostics;
using NeoSmart.PrettySize;
using System.IO.Compression;

using PDMediaFeed = AECMediaFeed.Schema.AEC_PollingDistricts.MediaFeed;
using EUMediaFeed = AECMediaFeed.Schema.AEC_Results.MediaFeed;

using HouseMediaFeedStructure = AECMediaFeed.Schema.AEC_Results.HouseMediaFeedStructure;

using ListViewSubItem = System.Windows.Forms.ListViewItem.ListViewSubItem;
using BrightIdeasSoftware;
using System.ComponentModel;
using AECMediaFeed.Schema.AEC_Results;

namespace AECMediaFeed;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        if (!DesignMode)
        {
            InitListViewPDFP();
        }
    }

    private void InitListViewPDFP()
    {
        var columnState = new OLVColumn("State", "State");
        var columnElectorate = new OLVColumn("Electorate", "Electorate")
        {
            Width = 100,
        };
        var columnCounted = new OLVColumn("Counted", "VotesCounted")
        {
            Width = 100,
        };
        var columnCountedPerc = new OLVColumn("%", "VotesCountedPerc");
        var columnCountedProgress = new OLVColumn("Progress", "VotesCountedProgress");
        var columnPartyName = new OLVColumn("Party", "PartyName")
        {
            Width = 200,
        };
        var columnPartyVotes = new OLVColumn("Votes", "PartyVotes");
        var columnPartyPercent = new OLVColumn("Percent", "PartyVotesPercent");
        var progressRenderer = new BarRenderer(0, 100 * 100);
        progressRenderer.UseStandardBar = true;
        columnCountedProgress.Renderer = progressRenderer;
        listViewPDFP.Columns.Add(columnState);
        listViewPDFP.Columns.Add(columnElectorate);
        listViewPDFP.Columns.Add(columnCounted);
        listViewPDFP.Columns.Add(columnCountedPerc);
        listViewPDFP.Columns.Add(columnCountedProgress);
        listViewPDFP.Columns.Add(columnPartyName);
        listViewPDFP.Columns.Add(columnPartyVotes);
        listViewPDFP.Columns.Add(columnPartyPercent);
        listViewPDFP.UseCellFormatEvents = true;
        listViewPDFP.FormatCell += (s, e) =>
        {
            if (e.Model is PDFPItem item)
            {
                if (e.Column.AspectName == "VotesCounted")
                {
                    e.SubItem.Text = $"{item.VotesCounted:n0}";
                }
                else if (e.Column.AspectName == "PartyVotes")
                {
                    e.SubItem.Text = $"{item.PartyVotes:n0}";
                }
            }
        };

        listViewOverallFP.Columns.AddRange(
            new OLVColumn("Party", "PartyName")
            {
                Width = 200
            },
            new OLVColumn("Votes", "PartyVotes")
            {
                Width = 100,
            },
            new OLVColumn("%", "PartyVotesPercent")
            {
                Width = 100
            },
            new OLVColumn("Overall %", "PartyVotesOverallPercent")
            {
                Width = 100
            },
            new OLVColumn("Seats", "Seats"),
            new OLVColumn("%", "SeatsPerc"));
        listViewOverallFP.UseCellFormatEvents = true;
        listViewOverallFP.FormatCell += (s, e) =>
        {
            if (e.Model is OverallFPItem item)
            {
                if (e.Column.AspectName == "PartyVotes")
                {
                    e.SubItem.Text = $"{item.PartyVotes:n0}";
                }
                else if (e.Column.AspectName == "SeatsPerc")
                {
                    e.SubItem.Text = $"{Math.Round(item.SeatsPerc, 2)}%";
                }
            }
        };
        Init_listViewInfoAffiliations();
        Init_listViewPerDistrictTwoParty();
    }

    public static Dictionary<string, string> AffiliationCodeLookup => new Dictionary<string, string>()
    {
        {"ALP", "Australian Labor Party" },
        {"LNP", "The Coalition" },
        {"AJP", "Animal Justice Party" },
        {"GRN", "The Greens" }
    };

    private void Init_listViewInfoAffiliations()
    {
        var colName = new OLVColumn("Name", nameof(AffiliationIdentifierStructure.RegisteredName))
        {
            Width = 300
        };
        var colShortCode = new OLVColumn("Code", nameof(AffiliationIdentifierStructure.ShortCode))
        {
            Width = 100
        };
        listViewInfoAffiliations.Columns.Add(colName);
        listViewInfoAffiliations.Columns.Add(colShortCode);
    }

    private void Init_listViewPerDistrictTwoParty()
    {
        listViewPerDistrictTwoParty.UseCellFormatEvents = true;
        listViewPerDistrictTwoParty.FormatCell += (s, e) =>
        {
            if (e.Model is PerDistrictTwoParty item)
            {
                switch (e.Column.AspectName)
                {
                    case nameof(PerDistrictTwoParty.VotesCounted):
                        e.SubItem.Text = $"{item.VotesCounted:n0}";
                        break;
                    case nameof(PerDistrictTwoParty.VotesCountedPerc):
                        e.SubItem.Text = $"{Math.Round(item.VotesCountedPerc, 2)}%";
                        break;
                    case nameof(PerDistrictTwoParty.PartyLeftVoteCount):
                        e.SubItem.Text = $"{item.PartyLeftVoteCount:n0}";
                        break;
                    case nameof(PerDistrictTwoParty.PartyLeftVotePerc):
                        e.SubItem.Text = $"{Math.Round(item.PartyLeftVotePerc, 2)}%";
                        break;
                    case nameof(PerDistrictTwoParty.PartyRightVoteCount):
                        e.SubItem.Text = $"{item.PartyRightVoteCount:n0}";
                        break;
                    case nameof(PerDistrictTwoParty.PartyRightVotePerc):
                        e.SubItem.Text = $"{Math.Round(item.PartyRightVotePerc, 2)}%";
                        break;
                }
            }
        };
        listViewPerDistrictTwoParty.Columns.AddRange(
            new OLVColumn("State", nameof(PerDistrictTwoParty.State)),
            new OLVColumn("Electorate", nameof(PerDistrictTwoParty.Electorate))
            {
                Width = 200
            },
            new OLVColumn("Total Votes Counted", nameof(PerDistrictTwoParty.VotesCounted))
            {
                Width = 120
            },
            new OLVColumn("%", nameof(PerDistrictTwoParty.VotesCountedPerc)),
            new OLVColumn("Party", nameof(PerDistrictTwoParty.PartyLeftName))
            {
                Width = 200
            },
            new OLVColumn("Votes", nameof(PerDistrictTwoParty.PartyLeftVoteCount)),
            new OLVColumn("%", nameof(PerDistrictTwoParty.PartyLeftVotePerc)),
            new OLVColumn("%", nameof(PerDistrictTwoParty.PartyRightVotePerc)),
            new OLVColumn("Votes", nameof(PerDistrictTwoParty.PartyRightVoteCount)),
            new OLVColumn("Party", nameof(PerDistrictTwoParty.PartyRightName))
            {
                Width = 200
            });
    }


    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsLocked { get; private set; }
    public void LockForm()
    {
        SetLockState(true);
    }
    public void UnlockForm()
    {
        SetLockState(false);
    }
    public void SetLockState(bool state)
    {
        IsLocked = state;
        menuStrip1.Enabled = !state;
        panelMain.Enabled = !state;
        UseWaitCursor = state;
    }

    public PDMediaFeed PollingDistricts = new();
    public EUMediaFeed ElectionResults = new();
    public DateTime LastUpdated = DateTimeOffset.FromUnixTimeSeconds(0).LocalDateTime;
    public DateTime FileLastUpdated = DateTimeOffset.FromUnixTimeMilliseconds(0).LocalDateTime;

    private bool TryGetCurrentElection(out EventResultsStructureElection? election)
    {
        var targetElectionName = comboBoxElectionName.SelectedItem?.ToString() ?? "";
        election = null;
        var eu = ElectionResults;
        foreach (var item in eu.Results.Election)
        {
            if (item.ElectionIdentifier.ElectionName == targetElectionName)
            {
                election = item;
                return true;
            }
        }

        return false;
    }

    private void SetWindowTitle(Schema.AEC_Results.EventIdentifierStructure? @event, Schema.AEC_Results.ElectionIdentifierStructure? election)
    {
        var value = "AEC Media Feed";
        if (@event != null)
        {
            value += $" - {@event.EventName}";
            if (election != null)
            {
                value += $" ({election.ElectionName})";
            }
        }
        Text = value;
    }

    private void RefreshPollingDistricts()
    {
        var pd = PollingDistricts;
        listViewPollingDistricts.BeginUpdate();
        listViewPollingDistricts.Items.Clear();
        if (pd.PollingDistrictList != null)
        {
            foreach (var item in pd.PollingDistrictList.PollingDistrict)
            {
                var lvi = listViewPollingDistricts.Items.Add(item.PollingDistrictIdentifier.StateIdentifier.Id.ToString());
                lvi.SubItems.Add(new ListViewSubItem(lvi, item.PollingDistrictIdentifier.Name.ToString()));
                lvi.SubItems.Add(new ListViewSubItem(lvi, $"{item.PollingPlaces.Length:n0}"));
            }
        }
        listViewPollingDistricts.EndUpdate();
    }

    private void RefreshControlsFromData()
    {
        RefreshPollingDistricts();
        var targetElectionName = comboBoxElectionName.SelectedItem?.ToString() ?? "";

        var eu = ElectionResults;
        var firstPreferenceCount = new Dictionary<string, long>();
        var firstPreferencePartyCount = new Dictionary<string, long>();
        var candidateAffiliations = new Dictionary<string, string>();
        var affiliationInfo = new Dictionary<string, Schema.AEC_Results.AffiliationIdentifierStructure>();
        var seatCount = new Dictionary<string, long>();
        long totalMaxVotes = 0;
        listViewPDFP.BeginUpdate();
        listViewPDFP.ClearObjects();
        listViewPerDistrictTwoParty.BeginUpdate();
        listViewPerDistrictTwoParty.ClearObjects();
        if (eu.Results != null)
        {
            Text = "AEC Media Feed - " + eu.Results.EventIdentifier.EventName;
            if (TryGetCurrentElection(out var election))
            {
                Text = $"AEC Media Feed - {eu.Results.EventIdentifier.EventName} ({election!.ElectionIdentifier.ElectionName})";
                if (election.Item is HouseMediaFeedStructure houseFeed)
                {
                    foreach (var item in houseFeed.Contests.Select(x =>
                    {
                        long countedInHere = 0;
                        if (long.TryParse(x.Enrolment.CloseOfRolls, out countedInHere))
                        {
                            totalMaxVotes += countedInHere;
                        }
                        var parsedVoteCount = x.TwoPartyPreferred.Select(e =>
                        {
                            if (long.TryParse(e.Votes.Value, out var v))
                                return v;
                            return -1;
                        }).ToArray();

                        var currentCountedSum = parsedVoteCount.Where(e => e >= 0).Sum();
                        var countedPerc = countedInHere == 0
                            ? 0
                            : ((decimal)currentCountedSum / countedInHere) * (decimal)100;

                        return new
                        {
                            Contest = x,
                            TwoPartyVoteCount = parsedVoteCount,
                            EnrollmentCount = countedInHere,
                            VotesCounted = currentCountedSum,
                            VotesCountedPerc = countedPerc,
                        };
                    }).OrderByDescending(e => e.VotesCountedPerc))
                    {
                        var x = item.Contest;
                        if (long.TryParse(x.Enrolment.CloseOfRolls, out var countedInHere))
                        {
                            totalMaxVotes += countedInHere;
                        }

                        var bestCandidate = x.FirstPreferences.Candidate.OrderByDescending(e => long.Parse(e.Votes.Value)).First();
                        var partyCode = bestCandidate.AffiliationIdentifier?.ShortCode ?? "";
                        if (!seatCount.ContainsKey(partyCode))
                        {
                            seatCount[partyCode] = 0;
                        }
                        seatCount[partyCode] += 1;
                        foreach (var preference in x.FirstPreferences.Candidate)
                        {
                            var key = preference.CandidateIdentifier.Id;
                            if (!firstPreferenceCount.ContainsKey(key))
                            {
                                firstPreferenceCount[key] = 0;
                            }
                            if (long.TryParse(preference.Votes.Value, out var v))
                            {
                                firstPreferenceCount[key] += v;
                            }

                            candidateAffiliations[preference.CandidateIdentifier.Id] = preference.AffiliationIdentifier?.ShortCode ?? "";
                            if (preference.AffiliationIdentifier != null)
                            {
                                if (!affiliationInfo.ContainsKey(preference.AffiliationIdentifier.ShortCode))
                                {
                                    affiliationInfo[preference.AffiliationIdentifier.ShortCode] = preference.AffiliationIdentifier;
                                }
                            }
                        }

                        if (x.TwoPartyPreferred.Length > 2 || x.TwoPartyPreferred.Length < 2)
                        {
                            Debug.WriteLine("UH OH, this contest has more than (or less than) 2 items in TwoPartyPreferred!!!! Contest: " + x.ContestIdentifier.ContestName);
                            Debugger.Break();
                        }

                        var maj = x.TwoPartyPreferred.OrderByDescending(e => long.Parse(e.Votes.Value)).First();

                        var majPerc = x.TwoPartyPreferred.First(e => e.CoalitionIdentifier == maj.CoalitionIdentifier);


                        listViewPDFP.AddObject(new PDFPItem()
                        {
                            State = x.PollingDistrictIdentifier.StateIdentifier.Id.ToString(),
                            Electorate = x.PollingDistrictIdentifier.Name,
                            VotesCounted = item.VotesCounted,
                            VotesCountedPerc = Math.Round(item.VotesCountedPerc, 2) + "%",
                            VotesCountedProgress = Convert.ToInt32(Math.Floor(item.VotesCountedPerc * 100)),
                            PartyName = majPerc.CoalitionIdentifier.CoalitionName,
                            PartyVotes = long.Parse(majPerc.Votes.Value),
                            PartyVotesPercent = Math.Round(majPerc.Votes.Percentage, 2) + "%"
                        });

                        var district = x.PollingDistrictIdentifier;

                        listViewPerDistrictTwoParty.AddObject(new PerDistrictTwoParty()
                        {
                            State = district.StateIdentifier.Id.ToString(),
                            Electorate = district.Name,
                            VotesCounted = item.VotesCounted,
                            VotesCountedPerc = item.VotesCountedPerc,

                            PartyLeftName = x.TwoPartyPreferred[0].CoalitionIdentifier.CoalitionName,
                            PartyLeftVoteCount = item.TwoPartyVoteCount[0],
                            PartyLeftVotePerc = x.TwoPartyPreferred[0].Votes.Percentage,

                            PartyRightName = x.TwoPartyPreferred[1].CoalitionIdentifier.CoalitionName,
                            PartyRightVoteCount = item.TwoPartyVoteCount[1],
                            PartyRightVotePerc = x.TwoPartyPreferred[1].Votes.Percentage
                        });
                    }
                }
            }
            SetWindowTitle(eu.Results.EventIdentifier, election?.ElectionIdentifier);
        }
        listViewPDFP.EndUpdate();
        listViewPerDistrictTwoParty.EndUpdate();

        long totalFirstPreferenceVoteCount = 0;
        foreach (var (a, b) in candidateAffiliations)
        {
            if (firstPreferenceCount.TryGetValue(a, out var v))
            {
                var key = b;
                if (!firstPreferencePartyCount.ContainsKey(key))
                    firstPreferencePartyCount[key] = 0;
                firstPreferencePartyCount[key] += v;
                totalFirstPreferenceVoteCount += v;
            }
        }

        var totalPerc = totalMaxVotes == 0
            ? 0
            : ((decimal)totalFirstPreferenceVoteCount / totalMaxVotes) * (decimal)100;
        labelVotesCounted.Text = $"Votes Counted ({Math.Round(totalPerc, 2)}%)";
        progressBarVotesCounted.Minimum = 0;
        progressBarVotesCounted.Maximum = 100*100;
        progressBarVotesCounted.Value = Convert.ToInt32(Math.Round(totalPerc * 100));

        listViewOverallFP.BeginUpdate();
        listViewOverallFP.ClearObjects();

        long totalSeatCount = seatCount.Select(e => e.Value).Sum();
        foreach (var (partyId, count) in firstPreferencePartyCount.OrderByDescending(e => e.Value))
        {
            var name = "Independent";
            if (affiliationInfo.TryGetValue(partyId, out var party))
            {
                name = party.RegisteredName;
                if (AffiliationCodeLookup.TryGetValue(partyId, out var p))
                {
                    name = p;
                }
            }
            var perc = totalFirstPreferenceVoteCount == 0
                ? 0
                : ((decimal)count / totalFirstPreferenceVoteCount) * (decimal)100;
            var overallPerc = totalMaxVotes == 0
                ? 0
                : ((decimal)count / totalMaxVotes) * (decimal)100;

            long seats = 0;
            if (seatCount.TryGetValue(partyId, out var x))
            {
                seats = x;
            }

            var seatsPerc = seats == 0
                ? 0
                : ((decimal)seats / totalSeatCount) * (decimal)100;

            listViewOverallFP.AddObject(new OverallFPItem()
            {
                PartyName = name,
                PartyVotes = count,
                PartyVotesPercent = Math.Round(perc, 2) + "%",
                PartyVotesOverallPercent = Math.Round(overallPerc, 2) + "%",
                Seats = seats,
                SeatsPerc = seatsPerc
            });
        }
        listViewOverallFP.EndUpdate();

        UpdateListViewInfoAffiliations();
    }

    private void UpdateListViewInfoAffiliations()
    {
        listViewInfoAffiliations.BeginUpdate();

        listViewInfoAffiliations.ClearObjects();

        var affiliationInfo = new HashSet<Schema.AEC_Results.AffiliationIdentifierStructure>();
        if (TryGetCurrentElection(out var election))
        {
            if (election!.Item is HouseMediaFeedStructure houseFeed)
            {
                foreach (var contest in houseFeed.Contests)
                {
                    foreach (var candidate in contest.FirstPreferences.Candidate)
                    {
                        if (candidate.AffiliationIdentifier != null)
                        {
                            if (!affiliationInfo.Any(e => e.ShortCode == candidate.AffiliationIdentifier.ShortCode && e.RegisteredName == candidate.AffiliationIdentifier.RegisteredName))
                            {
                                affiliationInfo.Add(candidate.AffiliationIdentifier);
                            }
                        }
                    }
                }
            }
        }

        listViewInfoAffiliations.AddObjects(affiliationInfo.ToList());

        listViewInfoAffiliations.EndUpdate();

        Trace.WriteLine($"Updated {nameof(listViewInfoAffiliations)} ({affiliationInfo.Count} rows)");
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        switch (keyData)
        {
            case Keys.F5:
                updateF5ToolStripMenuItem.PerformClick();
                return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void UpdateElements()
    {
        var selectedElectionName = comboBoxElectionName.SelectedItem?.ToString() ?? "";

        comboBoxElectionName.BeginUpdate();
        comboBoxElectionName.Items.Clear();
        var eu = ElectionResults;
        var index = 0;
        if (eu.Results != null)
        {
            foreach (var election in eu.Results.Election)
            {
                var name = election.ElectionIdentifier.ElectionName;
                comboBoxElectionName.Items.Add(election.ElectionIdentifier.ElectionName);
                if (name == selectedElectionName)
                {
                    comboBoxElectionName.SelectedIndex = index;
                }
                index++;
            }
        }
        comboBoxElectionName.EndUpdate();

        LastUpdated = DateTime.Now;
        labelLastUpdated.Text = $"Last Pulled: {LastUpdated} (AWST)".PadRight(80, ' ') + $"File Last Updated: {FileLastUpdated} (AWST)";

        RefreshControlsFromData();
    }

    private void PreloadData()
    {
        using var pdf = File.OpenRead(@"C:\Work\Personal\AECMediaFeed\aec-mediafeed-Standard-Preload-31496-20250501101148\xml\aec-mediafeed-pollingdistricts-31496.xml");
        PollingDistricts = ParseXml<PDMediaFeed>(pdf);
        using var euf = File.OpenRead(@"C:\Work\Personal\AECMediaFeed\aec-mediafeed-Standard-Preload-31496-20250501101148\xml\aec-mediafeed-results-standard-preload-31496.xml");
        ElectionResults = ParseXml<EUMediaFeed>(euf);

        using var resultFile = File.OpenRead(@"C:\Work\Personal\AECMediaFeed\aec-mediafeed-Standard-Verbose-31496-20250503200717\xml\aec-mediafeed-results-standard-verbose-31496.xml");
        ImportElectionResults(resultFile);
    }

    private void ImportElectionResults(Stream stream)
    {
        lock (ElectionResults)
        {
            ElectionResults = ParseXml<EUMediaFeed>(stream);
        }
    }

    private FtpClient? FtpClient;

    private string LatestDatasetZip = "";

    private void connectToAECServersToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (FtpClient == null)
            {
                FtpClient = new FtpClient("mediafeed.aec.gov.au");
                FtpClient.AutoConnect();
            }
            else
            {
                FtpClient.Disconnect();
                FtpClient.Connect();
            }
            Debug.WriteLine("Connected to AEC Media Feed");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this,
                ex.ToString(),
                "Failed to connect",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void ImportResultDataset(FtpClient client, FtpListItem file)
    {
        var ms = new MemoryStream(Convert.ToInt32(file.Size));
        Debug.WriteLine($"Starting download... (size: {PrettySize.Bytes(file.Size)}, file: {file.FullName})");
        client.DownloadStream(ms, file.FullName, progress: info =>
        {
            var items = new string[]
            {
                $"Progress: {Math.Round(info.Progress, 2)}%",
                $"ETA: {info.ETA}",
                $"Speed: {PrettySize.Bytes(Convert.ToInt32(Math.Round(info.TransferSpeed)))}/s"
            }.ToArray();
            var largestWidth = items.OrderByDescending(e => e.Length).Select(e => e.Length).First();
            Debug.WriteLine(string.Join(" ", items.Select(e => e.PadRight(largestWidth + 3, ' '))));
        });
        var electionId = file.FullName.Split('/')[1];
        var targetArchiveFilename = $"aec-mediafeed-results-standard-verbose-{electionId}.xml";

        ms.Seek(0, SeekOrigin.Begin);

        var archive = new ZipArchive(ms);
        var foundFile = false;
        foreach (var entry in archive.Entries)
        {
            if (entry.Name == targetArchiveFilename)
            {
                Debug.WriteLine("Found target file in archive! " + entry.FullName);
                foundFile = true;
                ImportElectionResults(entry.Open());
            }
        }
        if (!foundFile)
        {
            Invoke(() =>
            {
                MessageBox.Show(this,
                    string.Join("\r\n", "Could not find the following file in the following archive", "", "Filename: " + targetArchiveFilename, "Archive: " + file.FullName),
                    "AEC Archive Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            });
            ms.Dispose();
            return;
        }
        FileLastUpdated = file.Modified.Subtract(TimeSpan.FromHours(2));
        Debug.WriteLine("Successfully pulled latest data from the AEC!");
        Invoke(() =>
        {
            labelStatus.Text = "Updating Controls";
            UpdateElements();
        });
    }


    private object pullLock = new object();
    private void PullLatestData()
    {
        labelStatus.Text = "Connecting to AEC Media Feed FTP Server";
        connectToAECServersToolStripMenuItem.PerformClick();
        new Thread(() =>
        {
            this.Invoke(() =>
            {
                LockForm();
                labelStatus.Text = "Fetching files";
            });
            try
            {
                lock (pullLock)
                {
                    var listing = FtpClient!.GetListing("/31496/Standard/Verbose");
                    var firstFile = listing.OrderByDescending(e => e.Name).FirstOrDefault();
                    if (firstFile != null)
                    {
                        if (firstFile.Name != LatestDatasetZip)
                        {
                            Invoke(() =>
                            {
                                labelStatus.Text = "Downloading data";
                            });
                            Debug.WriteLine($"New file! Pulling data now :3 (filename: {firstFile.Name})");
                            ImportResultDataset(FtpClient!, firstFile);
                        }
                        else
                        {
                            Debug.WriteLine($"Latest remote file already matches ours (filename: {LatestDatasetZip})");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
            this.Invoke(() =>
            {
                UnlockForm();
                labelStatus.Text = "<idle>";
            });
        }).Start();
    }

    private void updateF5ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (IsLocked)
            return;
        PullLatestData();
    }

    private void importFromLocalDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (IsLocked)
            return;
        PreloadData();
        UpdateElements();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        connectToAECServersToolStripMenuItem.PerformClick();
    }

    private void refreshControlsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (IsLocked)
            return;
        RefreshControlsFromData();
    }

    private T ParseXml<T>(Stream fileStream)
        where T : class, new()
    {
        var ser = new XmlSerializer(typeof(T));
        var data = ser.Deserialize(fileStream);
        if (data is T x)
            return x;
        return new T();
    }
}
