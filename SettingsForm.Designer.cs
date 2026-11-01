namespace OllamaLmStudioBridge
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpOllama;
        private System.Windows.Forms.Label lblOllamaPath;
        private System.Windows.Forms.TextBox txtOllamaPath;
        private System.Windows.Forms.Button btnBrowseOllama;
        private System.Windows.Forms.Label lblOllamaStatus;
        private System.Windows.Forms.GroupBox grpLmStudio;
        private System.Windows.Forms.Label lblLmStudioPath;
        private System.Windows.Forms.TextBox txtLmStudioPath;
        private System.Windows.Forms.Button btnBrowseLmStudio;
        private System.Windows.Forms.Label lblLmStudioStatus;
        private System.Windows.Forms.GroupBox grpAdvanced;
        private System.Windows.Forms.Label lblLinkType;
        private System.Windows.Forms.ComboBox cmbLinkType;
        private System.Windows.Forms.Label lblSubFolder;
        private System.Windows.Forms.TextBox txtSubFolder;
        private System.Windows.Forms.CheckBox chkDryRun;
        private System.Windows.Forms.CheckBox chkAutoScan;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAutoDetect;
        private System.Windows.Forms.Button btnResetDefaults;
        private System.Windows.Forms.Label lblHint;

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
            this.grpOllama = new System.Windows.Forms.GroupBox();
            this.lblOllamaPath = new System.Windows.Forms.Label();
            this.txtOllamaPath = new System.Windows.Forms.TextBox();
            this.btnBrowseOllama = new System.Windows.Forms.Button();
            this.lblOllamaStatus = new System.Windows.Forms.Label();
            this.grpLmStudio = new System.Windows.Forms.GroupBox();
            this.lblLmStudioPath = new System.Windows.Forms.Label();
            this.txtLmStudioPath = new System.Windows.Forms.TextBox();
            this.btnBrowseLmStudio = new System.Windows.Forms.Button();
            this.lblLmStudioStatus = new System.Windows.Forms.Label();
            this.grpAdvanced = new System.Windows.Forms.GroupBox();
            this.lblLinkType = new System.Windows.Forms.Label();
            this.cmbLinkType = new System.Windows.Forms.ComboBox();
            this.lblSubFolder = new System.Windows.Forms.Label();
            this.txtSubFolder = new System.Windows.Forms.TextBox();
            this.chkDryRun = new System.Windows.Forms.CheckBox();
            this.chkAutoScan = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAutoDetect = new System.Windows.Forms.Button();
            this.btnResetDefaults = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // grpOllama
            // 
            this.grpOllama.Controls.Add(this.lblOllamaStatus);
            this.grpOllama.Controls.Add(this.btnBrowseOllama);
            this.grpOllama.Controls.Add(this.txtOllamaPath);
            this.grpOllama.Controls.Add(this.lblOllamaPath);
            this.grpOllama.Location = new System.Drawing.Point(16, 16);
            this.grpOllama.Name = "grpOllama";
            this.grpOllama.Size = new System.Drawing.Size(612, 105);
            this.grpOllama.TabIndex = 0;
            this.grpOllama.TabStop = false;
            this.grpOllama.Text = "Ollama Storage Configuration";

            // lblOllamaPath
            this.lblOllamaPath.AutoSize = true;
            this.lblOllamaPath.Location = new System.Drawing.Point(16, 28);
            this.lblOllamaPath.Text = "Ollama Models Root Directory (contains 'manifests' and 'blobs'):";

            // txtOllamaPath
            this.txtOllamaPath.Location = new System.Drawing.Point(19, 48);
            this.txtOllamaPath.Size = new System.Drawing.Size(485, 25);
            this.txtOllamaPath.TextChanged += new System.EventHandler(this.txtPaths_TextChanged);

            // btnBrowseOllama
            this.btnBrowseOllama.Location = new System.Drawing.Point(510, 46);
            this.btnBrowseOllama.Size = new System.Drawing.Size(86, 28);
            this.btnBrowseOllama.Text = "Browse...";
            this.btnBrowseOllama.UseVisualStyleBackColor = true;
            this.btnBrowseOllama.Click += new System.EventHandler(this.btnBrowseOllama_Click);

            // lblOllamaStatus
            this.lblOllamaStatus.AutoSize = true;
            this.lblOllamaStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblOllamaStatus.Location = new System.Drawing.Point(19, 78);

            // 
            // grpLmStudio
            // 
            this.grpLmStudio.Controls.Add(this.lblLmStudioStatus);
            this.grpLmStudio.Controls.Add(this.btnBrowseLmStudio);
            this.grpLmStudio.Controls.Add(this.txtLmStudioPath);
            this.grpLmStudio.Controls.Add(this.lblLmStudioPath);
            this.grpLmStudio.Location = new System.Drawing.Point(16, 132);
            this.grpLmStudio.Name = "grpLmStudio";
            this.grpLmStudio.Size = new System.Drawing.Size(612, 105);
            this.grpLmStudio.TabIndex = 1;
            this.grpLmStudio.TabStop = false;
            this.grpLmStudio.Text = "LM Studio Storage Configuration";

            // lblLmStudioPath
            this.lblLmStudioPath.AutoSize = true;
            this.lblLmStudioPath.Location = new System.Drawing.Point(16, 28);
            this.lblLmStudioPath.Text = "LM Studio Models Directory (contains model subfolders):";

            // txtLmStudioPath
            this.txtLmStudioPath.Location = new System.Drawing.Point(19, 48);
            this.txtLmStudioPath.Size = new System.Drawing.Size(485, 25);
            this.txtLmStudioPath.TextChanged += new System.EventHandler(this.txtPaths_TextChanged);

            // btnBrowseLmStudio
            this.btnBrowseLmStudio.Location = new System.Drawing.Point(510, 46);
            this.btnBrowseLmStudio.Size = new System.Drawing.Size(86, 28);
            this.btnBrowseLmStudio.Text = "Browse...";
            this.btnBrowseLmStudio.UseVisualStyleBackColor = true;
            this.btnBrowseLmStudio.Click += new System.EventHandler(this.btnBrowseLmStudio_Click);

            // lblLmStudioStatus
            this.lblLmStudioStatus.AutoSize = true;
            this.lblLmStudioStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblLmStudioStatus.Location = new System.Drawing.Point(19, 78);

            // 
            // grpAdvanced
            // 
            this.grpAdvanced.Controls.Add(this.chkAutoScan);
            this.grpAdvanced.Controls.Add(this.chkDryRun);
            this.grpAdvanced.Controls.Add(this.txtSubFolder);
            this.grpAdvanced.Controls.Add(this.lblSubFolder);
            this.grpAdvanced.Controls.Add(this.cmbLinkType);
            this.grpAdvanced.Controls.Add(this.lblLinkType);
            this.grpAdvanced.Location = new System.Drawing.Point(16, 248);
            this.grpAdvanced.Name = "grpAdvanced";
            this.grpAdvanced.Size = new System.Drawing.Size(612, 120);
            this.grpAdvanced.TabIndex = 2;
            this.grpAdvanced.TabStop = false;
            this.grpAdvanced.Text = "Linking Preferences";

            // lblLinkType
            this.lblLinkType.AutoSize = true;
            this.lblLinkType.Location = new System.Drawing.Point(16, 28);
            this.lblLinkType.Text = "Windows Link Mechanism:";

            // cmbLinkType
            this.cmbLinkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLinkType.Items.AddRange(new object[] {
                "Symbolic Link (Standard, requires Administrator or Developer Mode)",
                "Hard Link (No admin required, source & target must be on same drive)"
            });
            this.cmbLinkType.Location = new System.Drawing.Point(19, 48);
            this.cmbLinkType.Size = new System.Drawing.Size(430, 25);

            // lblSubFolder
            this.lblSubFolder.AutoSize = true;
            this.lblSubFolder.Location = new System.Drawing.Point(465, 28);
            this.lblSubFolder.Text = "Target Subfolder:";

            // txtSubFolder
            this.txtSubFolder.Location = new System.Drawing.Point(468, 48);
            this.txtSubFolder.Size = new System.Drawing.Size(128, 25);

            // chkDryRun
            this.chkDryRun.AutoSize = true;
            this.chkDryRun.Location = new System.Drawing.Point(19, 85);
            this.chkDryRun.Text = "Dry Run by default (simulate linking without modifying filesystem)";

            // chkAutoScan
            this.chkAutoScan.AutoSize = true;
            this.chkAutoScan.Location = new System.Drawing.Point(360, 85);
            this.chkAutoScan.Text = "Automatically scan on startup";

            // lblHint
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblHint.ForeColor = System.Drawing.Color.DimGray;
            this.lblHint.Location = new System.Drawing.Point(16, 375);
            this.lblHint.Size = new System.Drawing.Size(612, 28);
            this.lblHint.Text = "Configuration is persisted to %APPDATA%\\OllamaLmStudioBridge\\settings.json and reloaded on startup.";

            // btnAutoDetect
            this.btnAutoDetect.Location = new System.Drawing.Point(16, 412);
            this.btnAutoDetect.Size = new System.Drawing.Size(125, 32);
            this.btnAutoDetect.Text = "Auto-Detect";
            this.btnAutoDetect.Click += new System.EventHandler(this.btnAutoDetect_Click);

            // btnResetDefaults
            this.btnResetDefaults.Location = new System.Drawing.Point(148, 412);
            this.btnResetDefaults.Size = new System.Drawing.Size(125, 32);
            this.btnResetDefaults.Text = "Restore Defaults";
            this.btnResetDefaults.Click += new System.EventHandler(this.btnResetDefaults_Click);

            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(16, 124, 65);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(450, 412);
            this.btnSave.Size = new System.Drawing.Size(178, 32);
            this.btnSave.Text = "Save Configuration";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(345, 412);
            this.btnCancel.Size = new System.Drawing.Size(95, 32);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 460);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnResetDefaults);
            this.Controls.Add(this.btnAutoDetect);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.grpAdvanced);
            this.Controls.Add(this.grpLmStudio);
            this.Controls.Add(this.grpOllama);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration — Ollama & LM Studio Paths";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
