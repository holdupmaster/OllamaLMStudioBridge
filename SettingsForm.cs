using System;
using System.IO;
using System.Windows.Forms;

namespace OllamaLmStudioBridge
{
    public partial class SettingsForm : Form
    {
        public AppConfig CurrentConfig { get; private set; }

        public SettingsForm(AppConfig existingConfig)
        {
            InitializeComponent();
            CurrentConfig = existingConfig;
            PopulateControls();
        }

        private void PopulateControls()
        {
            txtOllamaPath.Text = CurrentConfig.OllamaModelsPath;
            txtLmStudioPath.Text = CurrentConfig.LmStudioModelsPath;
            txtSubFolder.Text = CurrentConfig.LmStudioSubFolder;
            cmbLinkType.SelectedIndex = CurrentConfig.LinkType == LinkMode.HardLink ? 1 : 0;
            chkDryRun.Checked = CurrentConfig.DryRun;
            chkAutoScan.Checked = CurrentConfig.AutoScanOnStartup;

            ValidatePaths();
        }

        private void ValidatePaths()
        {
            bool ollamaExists = Directory.Exists(txtOllamaPath.Text);
            lblOllamaStatus.Text = ollamaExists ? "✓ Folder exists on disk" : "⚠ Directory does not exist";
            lblOllamaStatus.ForeColor = ollamaExists ? System.Drawing.Color.ForestGreen : System.Drawing.Color.Firebrick;

            bool lmStudioExists = Directory.Exists(txtLmStudioPath.Text);
            lblLmStudioStatus.Text = lmStudioExists ? "✓ Folder exists on disk" : "ℹ Folder will be created if needed";
            lblLmStudioStatus.ForeColor = lmStudioExists ? System.Drawing.Color.ForestGreen : System.Drawing.Color.DimGray;
        }

        private void btnBrowseOllama_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select Ollama Models Directory (typically %USERPROFILE%\\.ollama\\models)";
                dialog.UseDescriptionForTitle = true;
                if (Directory.Exists(txtOllamaPath.Text))
                {
                    dialog.InitialDirectory = txtOllamaPath.Text;
                }

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtOllamaPath.Text = dialog.SelectedPath;
                    ValidatePaths();
                }
            }
        }

        private void btnBrowseLmStudio_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select LM Studio Models Directory (typically %USERPROFILE%\\.lmstudio\\models)";
                dialog.UseDescriptionForTitle = true;
                if (Directory.Exists(txtLmStudioPath.Text))
                {
                    dialog.InitialDirectory = txtLmStudioPath.Text;
                }

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtLmStudioPath.Text = dialog.SelectedPath;
                    ValidatePaths();
                }
            }
        }

        private void btnAutoDetect_Click(object sender, EventArgs e)
        {
            var defaults = AppConfig.GetDefaultConfig();
            txtOllamaPath.Text = defaults.OllamaModelsPath;
            txtLmStudioPath.Text = defaults.LmStudioModelsPath;
            ValidatePaths();

            MessageBox.Show(
                "Detected standard default directories for current user profile.\n\n" +
                $"Ollama: {defaults.OllamaModelsPath}\n" +
                $"LM Studio: {defaults.LmStudioModelsPath}",
                "Auto-Detect Paths",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnResetDefaults_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to reset all settings to defaults?",
                "Reset Configuration",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                var defaults = AppConfig.GetDefaultConfig();
                CurrentConfig = defaults;
                PopulateControls();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string ollama = txtOllamaPath.Text.Trim();
            string lmStudio = txtLmStudioPath.Text.Trim();

            if (string.IsNullOrEmpty(ollama))
            {
                MessageBox.Show("Please enter a valid Ollama models path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOllamaPath.Focus();
                return;
            }

            if (string.IsNullOrEmpty(lmStudio))
            {
                MessageBox.Show("Please enter a valid LM Studio models path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLmStudioPath.Focus();
                return;
            }

            // Update configuration object
            CurrentConfig.OllamaModelsPath = ollama;
            CurrentConfig.LmStudioModelsPath = lmStudio;
            CurrentConfig.LmStudioSubFolder = string.IsNullOrWhiteSpace(txtSubFolder.Text) ? "ollama" : txtSubFolder.Text.Trim();
            CurrentConfig.LinkType = cmbLinkType.SelectedIndex == 1 ? LinkMode.HardLink : LinkMode.SymbolicLink;
            CurrentConfig.DryRun = chkDryRun.Checked;
            CurrentConfig.AutoScanOnStartup = chkAutoScan.Checked;

            // Persist to disk (%APPDATA%\OllamaLmStudioBridge\settings.json)
            try
            {
                AppConfig.Save(CurrentConfig);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save settings: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtPaths_TextChanged(object sender, EventArgs e)
        {
            ValidatePaths();
        }
    }
}
