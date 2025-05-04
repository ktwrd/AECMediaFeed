namespace AECMediaFeed
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelLastUpdated = new Label();
            tabControl1 = new TabControl();
            tabPageResults = new TabPage();
            tabControl2 = new TabControl();
            tabPage1 = new TabPage();
            listViewOverallFP = new BrightIdeasSoftware.ObjectListView();
            tabPage2 = new TabPage();
            listViewPerDistrictTwoParty = new BrightIdeasSoftware.ObjectListView();
            tabPagePerDistrictFirstPreference = new TabPage();
            listViewPDFP = new BrightIdeasSoftware.ObjectListView();
            tabPagePollingDistricts = new TabPage();
            splitContainer1 = new SplitContainer();
            listViewPollingDistricts = new ListView();
            columnHeaderPDState = new ColumnHeader();
            columnHeaderPDName = new ColumnHeader();
            columnHeaderPDPlaceCount = new ColumnHeader();
            groupBoxPollingDistrictsMembers = new GroupBox();
            listViewPollingDistrictsMembers = new ListView();
            columnHeaderPDMemberName = new ColumnHeader();
            columnHeaderPDMemberParty = new ColumnHeader();
            tabPageInfo = new TabPage();
            tabControlInfo = new TabControl();
            tabPageInfoAffiliation = new TabPage();
            listViewInfoAffiliations = new BrightIdeasSoftware.ObjectListView();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            connectToAECServersToolStripMenuItem = new ToolStripMenuItem();
            updateF5ToolStripMenuItem = new ToolStripMenuItem();
            refreshControlsToolStripMenuItem = new ToolStripMenuItem();
            panelMain = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            comboBoxElectionName = new ComboBox();
            labelElection = new Label();
            labelVotesCounted = new Label();
            progressBarVotesCounted = new ProgressBar();
            labelStatusHeader = new Label();
            labelStatus = new Label();
            label1 = new Label();
            tabControl1.SuspendLayout();
            tabPageResults.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)listViewOverallFP).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)listViewPerDistrictTwoParty).BeginInit();
            tabPagePerDistrictFirstPreference.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)listViewPDFP).BeginInit();
            tabPagePollingDistricts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBoxPollingDistrictsMembers.SuspendLayout();
            tabPageInfo.SuspendLayout();
            tabControlInfo.SuspendLayout();
            tabPageInfoAffiliation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)listViewInfoAffiliations).BeginInit();
            menuStrip1.SuspendLayout();
            panelMain.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelLastUpdated
            // 
            labelLastUpdated.AutoSize = true;
            labelLastUpdated.Location = new Point(3, 723);
            labelLastUpdated.Margin = new Padding(3);
            labelLastUpdated.Name = "labelLastUpdated";
            labelLastUpdated.Size = new Size(113, 15);
            labelLastUpdated.TabIndex = 1;
            labelLastUpdated.Text = "Last Updated: Never";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageResults);
            tabControl1.Controls.Add(tabPagePollingDistricts);
            tabControl1.Controls.Add(tabPageInfo);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(3, 62);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1131, 655);
            tabControl1.TabIndex = 2;
            // 
            // tabPageResults
            // 
            tabPageResults.Controls.Add(tabControl2);
            tabPageResults.Location = new Point(4, 24);
            tabPageResults.Name = "tabPageResults";
            tabPageResults.Size = new Size(1123, 627);
            tabPageResults.TabIndex = 0;
            tabPageResults.Text = "Results";
            tabPageResults.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage1);
            tabControl2.Controls.Add(tabPage2);
            tabControl2.Controls.Add(tabPagePerDistrictFirstPreference);
            tabControl2.Dock = DockStyle.Fill;
            tabControl2.Location = new Point(0, 0);
            tabControl2.Margin = new Padding(0);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(1123, 627);
            tabControl2.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(listViewOverallFP);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1115, 599);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Overall (First Preference)";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // listViewOverallFP
            // 
            listViewOverallFP.Dock = DockStyle.Fill;
            listViewOverallFP.FullRowSelect = true;
            listViewOverallFP.GridLines = true;
            listViewOverallFP.Location = new Point(3, 3);
            listViewOverallFP.Name = "listViewOverallFP";
            listViewOverallFP.ShowGroups = false;
            listViewOverallFP.Size = new Size(1109, 593);
            listViewOverallFP.TabIndex = 1;
            listViewOverallFP.View = View.Details;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(listViewPerDistrictTwoParty);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1115, 599);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Per-District (Two-Party)";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // listViewPerDistrictTwoParty
            // 
            listViewPerDistrictTwoParty.Dock = DockStyle.Fill;
            listViewPerDistrictTwoParty.FullRowSelect = true;
            listViewPerDistrictTwoParty.GridLines = true;
            listViewPerDistrictTwoParty.Location = new Point(3, 3);
            listViewPerDistrictTwoParty.Name = "listViewPerDistrictTwoParty";
            listViewPerDistrictTwoParty.ShowGroups = false;
            listViewPerDistrictTwoParty.Size = new Size(1109, 593);
            listViewPerDistrictTwoParty.TabIndex = 1;
            listViewPerDistrictTwoParty.View = View.Details;
            // 
            // tabPagePerDistrictFirstPreference
            // 
            tabPagePerDistrictFirstPreference.Controls.Add(listViewPDFP);
            tabPagePerDistrictFirstPreference.Location = new Point(4, 24);
            tabPagePerDistrictFirstPreference.Name = "tabPagePerDistrictFirstPreference";
            tabPagePerDistrictFirstPreference.Padding = new Padding(3);
            tabPagePerDistrictFirstPreference.Size = new Size(1115, 599);
            tabPagePerDistrictFirstPreference.TabIndex = 2;
            tabPagePerDistrictFirstPreference.Text = "Per District (First Preference)";
            tabPagePerDistrictFirstPreference.UseVisualStyleBackColor = true;
            // 
            // listViewPDFP
            // 
            listViewPDFP.Dock = DockStyle.Fill;
            listViewPDFP.FullRowSelect = true;
            listViewPDFP.GridLines = true;
            listViewPDFP.HasCollapsibleGroups = false;
            listViewPDFP.Location = new Point(3, 3);
            listViewPDFP.Name = "listViewPDFP";
            listViewPDFP.ShowGroups = false;
            listViewPDFP.Size = new Size(1109, 593);
            listViewPDFP.TabIndex = 0;
            listViewPDFP.View = View.Details;
            // 
            // tabPagePollingDistricts
            // 
            tabPagePollingDistricts.Controls.Add(splitContainer1);
            tabPagePollingDistricts.Location = new Point(4, 24);
            tabPagePollingDistricts.Name = "tabPagePollingDistricts";
            tabPagePollingDistricts.Padding = new Padding(3);
            tabPagePollingDistricts.Size = new Size(1123, 627);
            tabPagePollingDistricts.TabIndex = 1;
            tabPagePollingDistricts.Text = "Polling Districts";
            tabPagePollingDistricts.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(listViewPollingDistricts);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBoxPollingDistrictsMembers);
            splitContainer1.Size = new Size(1117, 621);
            splitContainer1.SplitterDistance = 758;
            splitContainer1.TabIndex = 1;
            // 
            // listViewPollingDistricts
            // 
            listViewPollingDistricts.Columns.AddRange(new ColumnHeader[] { columnHeaderPDState, columnHeaderPDName, columnHeaderPDPlaceCount });
            listViewPollingDistricts.Dock = DockStyle.Fill;
            listViewPollingDistricts.FullRowSelect = true;
            listViewPollingDistricts.GridLines = true;
            listViewPollingDistricts.Location = new Point(0, 0);
            listViewPollingDistricts.Name = "listViewPollingDistricts";
            listViewPollingDistricts.Size = new Size(758, 621);
            listViewPollingDistricts.TabIndex = 0;
            listViewPollingDistricts.UseCompatibleStateImageBehavior = false;
            listViewPollingDistricts.View = View.Details;
            // 
            // columnHeaderPDState
            // 
            columnHeaderPDState.Text = "State";
            // 
            // columnHeaderPDName
            // 
            columnHeaderPDName.Text = "Name";
            columnHeaderPDName.Width = 200;
            // 
            // columnHeaderPDPlaceCount
            // 
            columnHeaderPDPlaceCount.Text = "Place Count";
            columnHeaderPDPlaceCount.Width = 100;
            // 
            // groupBoxPollingDistrictsMembers
            // 
            groupBoxPollingDistrictsMembers.Controls.Add(listViewPollingDistrictsMembers);
            groupBoxPollingDistrictsMembers.Dock = DockStyle.Fill;
            groupBoxPollingDistrictsMembers.Location = new Point(0, 0);
            groupBoxPollingDistrictsMembers.Name = "groupBoxPollingDistrictsMembers";
            groupBoxPollingDistrictsMembers.Size = new Size(355, 621);
            groupBoxPollingDistrictsMembers.TabIndex = 0;
            groupBoxPollingDistrictsMembers.TabStop = false;
            groupBoxPollingDistrictsMembers.Text = "Memberrs";
            // 
            // listViewPollingDistrictsMembers
            // 
            listViewPollingDistrictsMembers.Columns.AddRange(new ColumnHeader[] { columnHeaderPDMemberName, columnHeaderPDMemberParty });
            listViewPollingDistrictsMembers.Dock = DockStyle.Fill;
            listViewPollingDistrictsMembers.FullRowSelect = true;
            listViewPollingDistrictsMembers.GridLines = true;
            listViewPollingDistrictsMembers.Location = new Point(3, 19);
            listViewPollingDistrictsMembers.Name = "listViewPollingDistrictsMembers";
            listViewPollingDistrictsMembers.Size = new Size(349, 599);
            listViewPollingDistrictsMembers.TabIndex = 0;
            listViewPollingDistrictsMembers.UseCompatibleStateImageBehavior = false;
            listViewPollingDistrictsMembers.View = View.Details;
            // 
            // columnHeaderPDMemberName
            // 
            columnHeaderPDMemberName.Text = "Name";
            columnHeaderPDMemberName.Width = 200;
            // 
            // columnHeaderPDMemberParty
            // 
            columnHeaderPDMemberParty.Text = "Party";
            columnHeaderPDMemberParty.Width = 120;
            // 
            // tabPageInfo
            // 
            tabPageInfo.Controls.Add(tabControlInfo);
            tabPageInfo.Location = new Point(4, 24);
            tabPageInfo.Name = "tabPageInfo";
            tabPageInfo.Padding = new Padding(3);
            tabPageInfo.Size = new Size(1123, 627);
            tabPageInfo.TabIndex = 2;
            tabPageInfo.Text = "Info";
            tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // tabControlInfo
            // 
            tabControlInfo.Controls.Add(tabPageInfoAffiliation);
            tabControlInfo.Dock = DockStyle.Fill;
            tabControlInfo.Location = new Point(3, 3);
            tabControlInfo.Name = "tabControlInfo";
            tabControlInfo.SelectedIndex = 0;
            tabControlInfo.Size = new Size(1117, 621);
            tabControlInfo.TabIndex = 1;
            // 
            // tabPageInfoAffiliation
            // 
            tabPageInfoAffiliation.Controls.Add(listViewInfoAffiliations);
            tabPageInfoAffiliation.Location = new Point(4, 24);
            tabPageInfoAffiliation.Name = "tabPageInfoAffiliation";
            tabPageInfoAffiliation.Padding = new Padding(3);
            tabPageInfoAffiliation.Size = new Size(1109, 593);
            tabPageInfoAffiliation.TabIndex = 0;
            tabPageInfoAffiliation.Text = "Affiliations";
            tabPageInfoAffiliation.UseVisualStyleBackColor = true;
            // 
            // listViewInfoAffiliations
            // 
            listViewInfoAffiliations.Dock = DockStyle.Fill;
            listViewInfoAffiliations.FullRowSelect = true;
            listViewInfoAffiliations.GridLines = true;
            listViewInfoAffiliations.Location = new Point(3, 3);
            listViewInfoAffiliations.Name = "listViewInfoAffiliations";
            listViewInfoAffiliations.ShowGroups = false;
            listViewInfoAffiliations.Size = new Size(1103, 587);
            listViewInfoAffiliations.TabIndex = 0;
            listViewInfoAffiliations.View = View.Details;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Size = new Size(1137, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { connectToAECServersToolStripMenuItem, updateF5ToolStripMenuItem, refreshControlsToolStripMenuItem });
            fileToolStripMenuItem.Font = new Font("Segoe UI", 9F);
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // connectToAECServersToolStripMenuItem
            // 
            connectToAECServersToolStripMenuItem.Font = new Font("Segoe UI", 9F);
            connectToAECServersToolStripMenuItem.Name = "connectToAECServersToolStripMenuItem";
            connectToAECServersToolStripMenuItem.Size = new Size(198, 22);
            connectToAECServersToolStripMenuItem.Text = "Connect to AEC Servers";
            connectToAECServersToolStripMenuItem.Click += connectToAECServersToolStripMenuItem_Click;
            // 
            // updateF5ToolStripMenuItem
            // 
            updateF5ToolStripMenuItem.Font = new Font("Segoe UI", 9F);
            updateF5ToolStripMenuItem.Name = "updateF5ToolStripMenuItem";
            updateF5ToolStripMenuItem.Size = new Size(198, 22);
            updateF5ToolStripMenuItem.Text = "Update (F5)";
            updateF5ToolStripMenuItem.Click += updateF5ToolStripMenuItem_Click;
            // 
            // refreshControlsToolStripMenuItem
            // 
            refreshControlsToolStripMenuItem.Font = new Font("Segoe UI", 9F);
            refreshControlsToolStripMenuItem.Name = "refreshControlsToolStripMenuItem";
            refreshControlsToolStripMenuItem.Size = new Size(198, 22);
            refreshControlsToolStripMenuItem.Text = "Refresh Controls";
            refreshControlsToolStripMenuItem.Click += refreshControlsToolStripMenuItem_Click;
            // 
            // panelMain
            // 
            panelMain.ColumnCount = 1;
            panelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelMain.Controls.Add(tableLayoutPanel1, 0, 0);
            panelMain.Controls.Add(tabControl1, 0, 1);
            panelMain.Controls.Add(labelLastUpdated, 0, 2);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 24);
            panelMain.Name = "panelMain";
            panelMain.RowCount = 3;
            panelMain.RowStyles.Add(new RowStyle());
            panelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelMain.RowStyles.Add(new RowStyle());
            panelMain.Size = new Size(1137, 741);
            panelMain.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(comboBoxElectionName, 0, 1);
            tableLayoutPanel1.Controls.Add(labelElection, 0, 0);
            tableLayoutPanel1.Controls.Add(labelVotesCounted, 1, 0);
            tableLayoutPanel1.Controls.Add(progressBarVotesCounted, 1, 1);
            tableLayoutPanel1.Controls.Add(labelStatusHeader, 2, 0);
            tableLayoutPanel1.Controls.Add(labelStatus, 2, 1);
            tableLayoutPanel1.Controls.Add(label1, 3, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1131, 53);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // comboBoxElectionName
            // 
            comboBoxElectionName.FormattingEnabled = true;
            comboBoxElectionName.Location = new Point(4, 26);
            comboBoxElectionName.MinimumSize = new Size(200, 0);
            comboBoxElectionName.Name = "comboBoxElectionName";
            comboBoxElectionName.Size = new Size(200, 23);
            comboBoxElectionName.TabIndex = 3;
            // 
            // labelElection
            // 
            labelElection.AutoSize = true;
            labelElection.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelElection.Location = new Point(4, 4);
            labelElection.Margin = new Padding(3);
            labelElection.Name = "labelElection";
            labelElection.Size = new Size(51, 15);
            labelElection.TabIndex = 4;
            labelElection.Text = "Election";
            // 
            // labelVotesCounted
            // 
            labelVotesCounted.AutoSize = true;
            labelVotesCounted.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelVotesCounted.Location = new Point(211, 4);
            labelVotesCounted.Margin = new Padding(3);
            labelVotesCounted.Name = "labelVotesCounted";
            labelVotesCounted.Size = new Size(88, 15);
            labelVotesCounted.TabIndex = 5;
            labelVotesCounted.Text = "Votes Counted";
            // 
            // progressBarVotesCounted
            // 
            progressBarVotesCounted.Location = new Point(211, 26);
            progressBarVotesCounted.Name = "progressBarVotesCounted";
            progressBarVotesCounted.Size = new Size(100, 23);
            progressBarVotesCounted.TabIndex = 6;
            // 
            // labelStatusHeader
            // 
            labelStatusHeader.AutoSize = true;
            labelStatusHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelStatusHeader.Location = new Point(318, 4);
            labelStatusHeader.Margin = new Padding(3);
            labelStatusHeader.Name = "labelStatusHeader";
            labelStatusHeader.Size = new Size(42, 15);
            labelStatusHeader.TabIndex = 7;
            labelStatusHeader.Text = "Status";
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Dock = DockStyle.Fill;
            labelStatus.Location = new Point(318, 26);
            labelStatus.Margin = new Padding(3);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(65, 23);
            labelStatus.TabIndex = 8;
            labelStatus.Text = "<nothing>";
            labelStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(387, 1);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Padding = new Padding(3);
            tableLayoutPanel1.SetRowSpan(label1, 2);
            label1.Size = new Size(743, 51);
            label1.TabIndex = 9;
            label1.Text = "NOTE: Overall (First Preference) Seat Count may be inaccurate compared to other sources.\r\nI'm just giving the seats (internally) to who has the highest First-Preference (provided by the AEC).";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1137, 765);
            Controls.Add(panelMain);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AEC Media Feed";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPageResults.ResumeLayout(false);
            tabControl2.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)listViewOverallFP).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)listViewPerDistrictTwoParty).EndInit();
            tabPagePerDistrictFirstPreference.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)listViewPDFP).EndInit();
            tabPagePollingDistricts.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBoxPollingDistrictsMembers.ResumeLayout(false);
            tabPageInfo.ResumeLayout(false);
            tabControlInfo.ResumeLayout(false);
            tabPageInfoAffiliation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)listViewInfoAffiliations).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label labelLastUpdated;
        private TabControl tabControl1;
        private TabPage tabPageResults;
        private TabPage tabPagePollingDistricts;
        private ListView listViewPollingDistricts;
        private SplitContainer splitContainer1;
        private GroupBox groupBoxPollingDistrictsMembers;
        private ListView listViewPollingDistrictsMembers;
        private ColumnHeader columnHeaderPDState;
        private ColumnHeader columnHeaderPDName;
        private ColumnHeader columnHeaderPDPlaceCount;
        private ColumnHeader columnHeaderPDMemberName;
        private ColumnHeader columnHeaderPDMemberParty;
        private TabControl tabControl2;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem connectToAECServersToolStripMenuItem;
        private ToolStripMenuItem updateF5ToolStripMenuItem;
        private TableLayoutPanel panelMain;
        private ComboBox comboBoxElectionName;
        private ToolStripMenuItem refreshControlsToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelElection;
        private Label labelVotesCounted;
        private ProgressBar progressBarVotesCounted;
        private TabPage tabPagePerDistrictFirstPreference;
        private BrightIdeasSoftware.ObjectListView listViewPDFP;
        private Label labelStatusHeader;
        private Label labelStatus;
        private BrightIdeasSoftware.ObjectListView listViewOverallFP;
        private Label label1;
        private TabPage tabPageInfo;
        private BrightIdeasSoftware.ObjectListView listViewInfoAffiliations;
        private TabControl tabControlInfo;
        private TabPage tabPageInfoAffiliation;
        private BrightIdeasSoftware.ObjectListView listViewPerDistrictTwoParty;
    }
}
