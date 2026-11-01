using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OllamaLmStudioBridge
{
    public partial class MainForm : Form
    {
        private AppConfig _config;
        private readonly ModelLinkService _linkService;
        private List<OllamaModelItem> _discoveredModels = new();
        private CancellationTokenSource? _cts;
        private readonly bool _isAdmin;

        public MainForm()
        {
            InitializeComponent();
            _isAdmin = IsAdministrator();
            _linkService = new ModelLinkService();
            _linkService.OnLog += LinkService_OnLog;

            // Load persisted configuration from %APPDATA%\OllamaLmStudioBridge\settings.json
            _config = AppConfig.Load();
        }

        private static bool IsAdministrator()
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateAdminStatusIndicator();
            UpdatePathSummaryLabel();

            if (_config.AutoScanOnStartup)
            {
                btnScan_Click(sender, e);
            }
        }

        private void UpdateAdminStatusIndicator()
        {
            if (_isAdmin)
            {
                lblAdminBadge.Text = "🛡️ Elevated (Admin)";
                lblAdminBadge.ForeColor = Color.ForestGreen;
                btnRestartAdmin.Visible = false;
            }
            else
            {
                lblAdminBadge.Text = "⚠️ Non-Admin";
                lblAdminBadge.ForeColor = Color.DarkOrange;
                btnRestartAdmin.Visible = true;
            }
        }

        private void UpdatePathSummaryLabel()
        {
            lblOllamaPathSummary.Text = $"Ollama: {_config.OllamaModelsPath}";
            lblLmStudioPathSummary.Text = $"LM Studio: {_config.LmStudioModelsPath}";
            toolTip1.SetToolTip(lblOllamaPathSummary, _config.OllamaModelsPath);
            toolTip1.SetToolTip(lblLmStudioPathSummary, _config.LmStudioModelsPath);
        }

        private void LinkService_OnLog(object? sender, LogMessageEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => LinkService_OnLog(sender, e)));
                return;
            }

            Color logColor = e.Level switch
            {
                "SUCCESS" => Color.DarkGreen,
                "WARNING" => Color.DarkGoldenrod,
                "ERROR" => Color.Crimson,
                _ => Color.Black
            };

            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            txtConsoleLog.SelectionStart = txtConsoleLog.TextLength;
            txtConsoleLog.SelectionLength = 0;
            txtConsoleLog.SelectionColor = Color.Gray;
            txtConsoleLog.AppendText($"[{timestamp}] ");

            txtConsoleLog.SelectionColor = logColor;
            txtConsoleLog.AppendText($"[{e.Level}] {e.Message}\r\n");
            txtConsoleLog.ScrollToCaret();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using var settingsForm = new SettingsForm(_config);
            if (settingsForm.ShowDialog(this) == DialogResult.OK)
            {
                _config = settingsForm.CurrentConfig;
                UpdatePathSummaryLabel();
                LogToConsole("Configuration saved and active.", "SUCCESS");

                // Prompt to re-scan with new directories
                var res = MessageBox.Show(
                    "Paths have been updated. Would you like to scan for Ollama models in the new path now?",
                    "Scan New Paths?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (res == DialogResult.Yes)
                {
                    btnScan_Click(sender, e);
                }
            }
        }

        private async void btnScan_Click(object sender, EventArgs e)
        {
            SetBusyState(true, "Scanning Ollama models directory...");
            _cts = new CancellationTokenSource();

            try
            {
                _discoveredModels = await _linkService.ScanModelsAsync(_config, _cts.Token);
                PopulateListView();
                UpdateStatusSummary();
            }
            catch (OperationCanceledException)
            {
                LogToConsole("Scan operation cancelled by user.", "WARNING");
            }
            catch (Exception ex)
            {
                LogToConsole($"Scan failed: {ex.Message}", "ERROR");
                MessageBox.Show($"Scan error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusyState(false, $"Ready. {_discoveredModels.Count} models found.");
            }
        }

        private void PopulateListView()
        {
            listViewModels.BeginUpdate();
            listViewModels.Items.Clear();

            string search = txtSearch.Text.Trim().ToLowerInvariant();

            foreach (var model in _discoveredModels)
            {
                if (!string.IsNullOrEmpty(search) && !model.FullName.ToLowerInvariant().Contains(search))
                {
                    continue;
                }

                var item = new ListViewItem(model.Family)
                {
                    Tag = model,
                    Checked = model.IsSelected
                };
                item.SubItems.Add(model.Tag);
                item.SubItems.Add(model.FormattedSize);
                item.SubItems.Add(model.StatusMessage);
                item.SubItems.Add(model.DigestHash.Length > 19 ? model.DigestHash.Substring(0, 19) + "..." : model.DigestHash);
                item.SubItems.Add(model.TargetSymlinkPath);

                if (model.Status == ModelLinkStatus.Linked)
                {
                    item.ForeColor = Color.ForestGreen;
                }
                else if (model.Status == ModelLinkStatus.MissingBlob)
                {
                    item.ForeColor = Color.Firebrick;
                }

                listViewModels.Items.Add(item);
            }

            listViewModels.EndUpdate();
        }

        private async void btnLinkSelected_Click(object sender, EventArgs e)
        {
            var selectedModels = _discoveredModels.Where(m => m.IsSelected).ToList();
            if (selectedModels.Count == 0)
            {
                MessageBox.Show("Please select at least one model to link.", "No Models Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string prompt = $"Link {selectedModels.Count} selected model(s) to LM Studio using {_config.LinkType}?\n\nTarget: {_config.GetEffectiveLmStudioOllamaRoot()}";
            if (MessageBox.Show(prompt, "Confirm Link Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            SetBusyState(true, $"Linking {selectedModels.Count} models...");
            progressBar.Value = 0;
            var progress = new Progress<int>(percent => progressBar.Value = Math.Clamp(percent, 0, 100));
            _cts = new CancellationTokenSource();

            try
            {
                int linked = await _linkService.LinkModelsAsync(selectedModels, _config, progress, _cts.Token);
                LogToConsole($"Linking complete. {linked} model(s) linked successfully.", "SUCCESS");
                PopulateListView();
                UpdateStatusSummary();
            }
            catch (Exception ex)
            {
                LogToConsole($"Linking operation failed: {ex.Message}", "ERROR");
            }
            finally
            {
                SetBusyState(false, "Ready.");
            }
        }

        private async void btnUnlinkSelected_Click(object sender, EventArgs e)
        {
            var selectedModels = _discoveredModels.Where(m => m.IsSelected).ToList();
            if (selectedModels.Count == 0)
            {
                MessageBox.Show("Please select at least one model to unlink.", "No Models Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var linkedModels = selectedModels.Where(m => m.Status == ModelLinkStatus.Linked || File.Exists(m.TargetSymlinkPath)).ToList();
            if (linkedModels.Count == 0)
            {
                MessageBox.Show("None of the selected models are currently linked in LM Studio.", "Not Linked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string prompt = $"Unlink {linkedModels.Count} model(s) from LM Studio?\n\n" +
                            "This removes the symlinks/hardlinks inside LM Studio.\n" +
                            "SAFETY NOTE: Original Ollama models and blobs will NOT be deleted.";

            if (MessageBox.Show(prompt, "Confirm Unlink", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            SetBusyState(true, $"Unlinking {linkedModels.Count} models...");
            progressBar.Value = 0;
            var progress = new Progress<int>(percent => progressBar.Value = Math.Clamp(percent, 0, 100));
            _cts = new CancellationTokenSource();

            try
            {
                int unlinked = await _linkService.UnlinkModelsAsync(linkedModels, _config, progress, _cts.Token);
                LogToConsole($"Unlinking complete. {unlinked} model(s) unlinked.", "SUCCESS");
                PopulateListView();
                UpdateStatusSummary();
            }
            catch (Exception ex)
            {
                LogToConsole($"Unlinking operation failed: {ex.Message}", "ERROR");
            }
            finally
            {
                SetBusyState(false, "Ready.");
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var m in _discoveredModels) m.IsSelected = true;
            foreach (ListViewItem item in listViewModels.Items) item.Checked = true;
            UpdateStatusSummary();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (var m in _discoveredModels) m.IsSelected = false;
            foreach (ListViewItem item in listViewModels.Items) item.Checked = false;
            UpdateStatusSummary();
        }

        private void listViewModels_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag is OllamaModelItem model)
            {
                model.IsSelected = e.Item.Checked;
                UpdateStatusSummary();
            }
        }

        private void UpdateStatusSummary()
        {
            int total = _discoveredModels.Count;
            int selected = _discoveredModels.Count(m => m.IsSelected);
            int linked = _discoveredModels.Count(m => m.Status == ModelLinkStatus.Linked);

            lblModelStats.Text = $"Total: {total} | Selected: {selected} | Already Linked: {linked}";
        }

        private void SetBusyState(bool busy, string statusText)
        {
            btnScan.Enabled = !busy;
            btnLinkSelected.Enabled = !busy;
            btnUnlinkSelected.Enabled = !busy;
            btnSettings.Enabled = !busy;
            progressBar.Visible = busy;
            lblStatus.Text = statusText;
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }

        private void LogToConsole(string message, string level = "INFO")
        {
            LinkService_OnLog(this, new LogMessageEventArgs(message, level));
        }

        private void btnRestartAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    UseShellExecute = true,
                    Verb = "runas" // Elevates to Administrator
                };
                Process.Start(psi);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not elevate: {ex.Message}", "UAC Elevation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnOpenOllamaFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(_config.OllamaModelsPath))
                Process.Start("explorer.exe", _config.OllamaModelsPath);
            else
                MessageBox.Show($"Directory does not exist: {_config.OllamaModelsPath}", "Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnOpenLmStudioFolder_Click(object sender, EventArgs e)
        {
            string path = _config.GetEffectiveLmStudioOllamaRoot();
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            Process.Start("explorer.exe", path);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtConsoleLog.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            PopulateListView();
        }
    }
}
