namespace OllamaLmStudioBridge
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkSelectedMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnScan;
        private System.Windows.Forms.ToolStripButton btnLinkSelected;
        private System.Windows.Forms.ToolStripButton btnUnlinkSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripButton btnOpenOllama;
        private System.Windows.Forms.ToolStripButton btnOpenLmStudio;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnRestartAdmin;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel lblModelStats;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblOllamaPathSummary;
        private System.Windows.Forms.Label lblLmStudioPathSummary;
        private System.Windows.Forms.Button btnChangePathsQuick;
        private System.Windows.Forms.Label lblAdminBadge;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listViewModels;
        private System.Windows.Forms.ColumnHeader colFamily;
        private System.Windows.Forms.ColumnHeader colTag;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colDigest;
        private System.Windows.Forms.ColumnHeader colTarget;
        private System.Windows.Forms.Panel panelListHeader;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.RichTextBox txtConsoleLog;
        private System.Windows.Forms.Panel panelLogHeader;
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.ToolTip toolTip1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkSelectedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnScan = new System.Windows.Forms.ToolStripButton();
            this.btnLinkSelected = new System.Windows.Forms.ToolStripButton();
            this.btnUnlinkSelected = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.btnOpenOllama = new System.Windows.Forms.ToolStripButton();
            this.btnOpenLmStudio = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRestartAdmin = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblModelStats = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblAdminBadge = new System.Windows.Forms.Label();
            this.btnChangePathsQuick = new System.Windows.Forms.Button();
            this.lblLmStudioPathSummary = new System.Windows.Forms.Label();
            this.lblOllamaPathSummary = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewModels = new System.Windows.Forms.ListView();
            this.colFamily = new System.Windows.Forms.ColumnHeader();
            this.colTag = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.colDigest = new System.Windows.Forms.ColumnHeader();
            this.colTarget = new System.Windows.Forms.ColumnHeader();
            this.panelListHeader = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.txtConsoleLog = new System.Windows.Forms.RichTextBox();
            this.panelLogHeader = new System.Windows.Forms.Panel();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.lblLogTitle = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);

            this.SuspendLayout();

            // MenuStrip
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileToolStripMenuItem,
                this.actionsToolStripMenuItem
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1080, 24);

            // File Menu
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.settingsMenuItem,
                this.toolStripSeparator1,
                this.exitMenuItem
            });
            this.fileToolStripMenuItem.Text = "&File";

            this.settingsMenuItem.Text = "&Configuration...";
            this.settingsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            this.settingsMenuItem.Click += new System.EventHandler(this.btnSettings_Click);

            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += (s, e) => Application.Exit();

            // Actions Menu
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.scanMenuItem,
                this.linkSelectedMenuItem,
                this.unlinkMenuItem
            });
            this.actionsToolStripMenuItem.Text = "&Actions";

            this.scanMenuItem.Text = "&Scan Ollama Models";
            this.scanMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.scanMenuItem.Click += new System.EventHandler(this.btnScan_Click);

            this.linkSelectedMenuItem.Text = "&Link Selected Models";
            this.linkSelectedMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L;
            this.linkSelectedMenuItem.Click += new System.EventHandler(this.btnLinkSelected_Click);

            this.unlinkMenuItem.Text = "&Unlink Selected Models";
            this.unlinkMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U;
            this.unlinkMenuItem.Click += new System.EventHandler(this.btnUnlinkSelected_Click);

            // ToolStrip
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.btnScan,
                this.btnLinkSelected,
                this.btnUnlinkSelected,
                this.toolStripSeparator2,
                this.btnSettings,
                this.btnOpenOllama,
                this.btnOpenLmStudio,
                this.toolStripSeparator3,
                this.btnRestartAdmin
            });
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Size = new System.Drawing.Size(1080, 28);

            this.btnScan.Text = "🔍 Scan Models";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);

            this.btnLinkSelected.Text = "🔗 Link Selected";
            this.btnLinkSelected.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLinkSelected.Click += new System.EventHandler(this.btnLinkSelected_Click);

            this.btnUnlinkSelected.Text = "❌ Unlink Selected";
            this.btnUnlinkSelected.Click += new System.EventHandler(this.btnUnlinkSelected_Click);

            this.btnSettings.Text = "⚙️ Configuration";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);

            this.btnOpenOllama.Text = "📁 Open Ollama";
            this.btnOpenOllama.Click += new System.EventHandler(this.btnOpenOllamaFolder_Click);

            this.btnOpenLmStudio.Text = "📁 Open LM Studio";
            this.btnOpenLmStudio.Click += new System.EventHandler(this.btnOpenLmStudioFolder_Click);

            this.btnRestartAdmin.Text = "🛡️ Elevate to Admin";
            this.btnRestartAdmin.Click += new System.EventHandler(this.btnRestartAdmin_Click);

            // Top Path Summary Bar
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.panelTop.Controls.Add(this.lblAdminBadge);
            this.panelTop.Controls.Add(this.btnChangePathsQuick);
            this.panelTop.Controls.Add(this.lblLmStudioPathSummary);
            this.panelTop.Controls.Add(this.lblOllamaPathSummary);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 52);
            this.panelTop.Size = new System.Drawing.Size(1080, 62);
            this.panelTop.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);

            // Row 1: Ollama Path
            this.lblOllamaPathSummary.AutoEllipsis = true;
            this.lblOllamaPathSummary.Location = new System.Drawing.Point(14, 10);
            this.lblOllamaPathSummary.Size = new System.Drawing.Size(640, 20);
            this.lblOllamaPathSummary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOllamaPathSummary.Text = "Ollama: ...";

            // Row 2: LM Studio Path
            this.lblLmStudioPathSummary.AutoEllipsis = true;
            this.lblLmStudioPathSummary.Location = new System.Drawing.Point(14, 34);
            this.lblLmStudioPathSummary.Size = new System.Drawing.Size(640, 20);
            this.lblLmStudioPathSummary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLmStudioPathSummary.Text = "LM Studio: ...";

            // Change Paths Button (Spacious 115x32px)
            this.btnChangePathsQuick.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnChangePathsQuick.Location = new System.Drawing.Point(800, 14);
            this.btnChangePathsQuick.Size = new System.Drawing.Size(120, 32);
            this.btnChangePathsQuick.Text = "Change Paths...";
            this.btnChangePathsQuick.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChangePathsQuick.UseVisualStyleBackColor = true;
            this.btnChangePathsQuick.Click += new System.EventHandler(this.btnSettings_Click);

            // Admin Badge
            this.lblAdminBadge.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.lblAdminBadge.Location = new System.Drawing.Point(925, 20);
            this.lblAdminBadge.Size = new System.Drawing.Size(145, 22);
            this.lblAdminBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminBadge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // SplitContainer
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.SplitterDistance = 380;
            this.splitContainer1.Location = new System.Drawing.Point(0, 114);
            this.splitContainer1.Size = new System.Drawing.Size(1080, 514);

            // Panel List Header (Height increased to 48px to prevent button and label clipping)
            this.panelListHeader.Controls.Add(this.lblSearch);
            this.panelListHeader.Controls.Add(this.txtSearch);
            this.panelListHeader.Controls.Add(this.btnDeselectAll);
            this.panelListHeader.Controls.Add(this.btnSelectAll);
            this.panelListHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelListHeader.Height = 48;
            this.panelListHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);

            this.btnSelectAll.Location = new System.Drawing.Point(14, 9);
            this.btnSelectAll.Size = new System.Drawing.Size(95, 30);
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);

            this.btnDeselectAll.Location = new System.Drawing.Point(118, 9);
            this.btnDeselectAll.Size = new System.Drawing.Size(105, 30);
            this.btnDeselectAll.Text = "Deselect All";
            this.btnDeselectAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);

            this.lblSearch.Location = new System.Drawing.Point(245, 15);
            this.lblSearch.Text = "Filter:";
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.txtSearch.Location = new System.Drawing.Point(290, 11);
            this.txtSearch.Size = new System.Drawing.Size(260, 26);
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // ListView
            this.listViewModels.CheckBoxes = true;
            this.listViewModels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colFamily,
                this.colTag,
                this.colSize,
                this.colStatus,
                this.colDigest,
                this.colTarget
            });
            this.listViewModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewModels.FullRowSelect = true;
            this.listViewModels.GridLines = true;
            this.listViewModels.View = System.Windows.Forms.View.Details;
            this.listViewModels.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewModels_ItemChecked);

            this.colFamily.Text = "Model Family";
            this.colFamily.Width = 140;
            this.colTag.Text = "Tag";
            this.colTag.Width = 100;
            this.colSize.Text = "GGUF Size";
            this.colSize.Width = 100;
            this.colStatus.Text = "Status";
            this.colStatus.Width = 120;
            this.colDigest.Text = "Blob Digest";
            this.colDigest.Width = 180;
            this.colTarget.Text = "LM Studio Destination Path";
            this.colTarget.Width = 380;

            this.splitContainer1.Panel1.Controls.Add(this.listViewModels);
            this.splitContainer1.Panel1.Controls.Add(this.panelListHeader);

            // Console Log Bottom Panel
            this.panelLogHeader.Controls.Add(this.btnClearLog);
            this.panelLogHeader.Controls.Add(this.lblLogTitle);
            this.panelLogHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogHeader.Height = 32;

            this.lblLogTitle.Text = "Activity & Diagnostic Output Log:";
            this.lblLogTitle.Location = new System.Drawing.Point(10, 8);
            this.lblLogTitle.AutoSize = true;
            this.lblLogTitle.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.btnClearLog.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnClearLog.Location = new System.Drawing.Point(990, 4);
            this.btnClearLog.Size = new System.Drawing.Size(80, 24);
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);

            this.txtConsoleLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsoleLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtConsoleLog.ReadOnly = true;
            this.txtConsoleLog.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);

            this.splitContainer1.Panel2.Controls.Add(this.txtConsoleLog);
            this.splitContainer1.Panel2.Controls.Add(this.panelLogHeader);

            // StatusStrip
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblStatus,
                this.progressBar,
                this.lblModelStats
            });
            this.statusStrip1.Location = new System.Drawing.Point(0, 628);
            this.statusStrip1.Size = new System.Drawing.Size(1080, 24);

            this.lblStatus.Spring = true;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Text = "Ready.";

            this.progressBar.Size = new System.Drawing.Size(180, 16);
            this.progressBar.Visible = false;

            this.lblModelStats.Text = "Total: 0 | Selected: 0 | Linked: 0";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 680);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ollama to LM Studio Bridge (.NET Windows Forms)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
