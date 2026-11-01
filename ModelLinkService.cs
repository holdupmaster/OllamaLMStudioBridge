using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OllamaLmStudioBridge
{
    public class LogMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
        public string Level { get; set; } // "INFO", "SUCCESS", "WARNING", "ERROR"

        public LogMessageEventArgs(string message, string level = "INFO")
        {
            Message = message;
            Level = level;
        }
    }

    public class ModelLinkService
    {
        public event EventHandler<LogMessageEventArgs>? OnLog;

        #region Win32 API Interop for Symlinks and Hardlinks
        [DllImport("kernel32.dll", EntryPoint = "CreateSymbolicLinkW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

        [DllImport("kernel32.dll", EntryPoint = "CreateHardLinkW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

        // 0x0 = File, 0x1 = Directory, 0x2 = Allow unprivileged create (Developer Mode enabled on Windows 10/11)
        private const int SYMBOLIC_LINK_FLAG_FILE = 0x0;
        private const int SYMBOLIC_LINK_FLAG_ALLOW_UNPRIVILEGED_CREATE = 0x2;
        #endregion

        private void Log(string message, string level = "INFO")
        {
            OnLog?.Invoke(this, new LogMessageEventArgs(message, level));
        }

        /// <summary>
        /// Asynchronously scans the Ollama directory for manifests and resolves GGUF blob paths.
        /// Corresponds directly to main() manifest iteration in the original Python script.
        /// </summary>
        public async Task<List<OllamaModelItem>> ScanModelsAsync(AppConfig config, CancellationToken cancellationToken = default)
        {
            var results = new List<OllamaModelItem>();

            string ollamaRoot = config.OllamaModelsPath;
            string lmStudioRoot = config.GetEffectiveLmStudioOllamaRoot();

            Log($"Scanning Ollama model directory: {ollamaRoot}");

            if (!Directory.Exists(ollamaRoot))
            {
                Log($"Ollama root directory does not exist: {ollamaRoot}", "ERROR");
                return results;
            }

            // In Ollama: manifests/registry.ollama.ai/library/{family}/{tag}
            string manifestsLibraryPath = Path.Combine(ollamaRoot, "manifests", "registry.ollama.ai", "library");
            string blobsPath = Path.Combine(ollamaRoot, "blobs");

            if (!Directory.Exists(manifestsLibraryPath))
            {
                // Also check if user pointed directly to registry.ollama.ai or library
                if (Directory.Exists(Path.Combine(ollamaRoot, "library")))
                    manifestsLibraryPath = Path.Combine(ollamaRoot, "library");
                else
                {
                    Log($"Warning: Library manifest directory not found at {manifestsLibraryPath}. Trying root scan...", "WARNING");
                    manifestsLibraryPath = ollamaRoot;
                }
            }

            await Task.Run(() =>
            {
                try
                {
                    // Scan each model family folder
                    var familyDirs = Directory.GetDirectories(manifestsLibraryPath);
                    foreach (var familyDir in familyDirs)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        string familyName = Path.GetFileName(familyDir);

                        // Scan each tag manifest file within the family directory
                        var tagFiles = Directory.GetFiles(familyDir);
                        foreach (var tagFile in tagFiles)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            string tagName = Path.GetFileName(tagFile);

                            try
                            {
                                var model = ParseManifestFile(tagFile, familyName, tagName, blobsPath, lmStudioRoot);
                                if (model != null)
                                {
                                    results.Add(model);
                                    Log($"Discovered Ollama model '{model.FullName}' with GGUF at {model.GgufBlobPath}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Log($"WARNING: Failed to parse Ollama tag manifest at {tagFile}: {ex.Message}", "WARNING");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error scanning manifests: {ex.Message}", "ERROR");
                }
            }, cancellationToken);

            Log($"Discovered {results.Count} models across Ollama library.", "SUCCESS");
            return results;
        }

        private OllamaModelItem? ParseManifestFile(string manifestFilePath, string family, string tag, string blobsPath, string lmStudioRoot)
        {
            string jsonContent = File.ReadAllText(manifestFilePath);
            using var doc = JsonDocument.Parse(jsonContent);
            var root = doc.RootElement;

            if (!root.TryGetProperty("layers", out var layersElement) || layersElement.ValueKind != JsonValueKind.Array)
            {
                return null;
            }

            // Find layer with mediaType == "application/vnd.ollama.image.model"
            string? digest = null;
            long size = 0;

            foreach (var layer in layersElement.EnumerateArray())
            {
                if (layer.TryGetProperty("mediaType", out var mediaTypeElem) &&
                    mediaTypeElem.GetString() == "application/vnd.ollama.image.model")
                {
                    if (layer.TryGetProperty("digest", out var digestElem))
                    {
                        digest = digestElem.GetString();
                    }
                    if (layer.TryGetProperty("size", out var sizeElem))
                    {
                        size = sizeElem.GetInt64();
                    }
                    break;
                }
            }

            if (string.IsNullOrEmpty(digest))
            {
                return null;
            }

            // Digest format is "sha256:abcd1234...", on disk Ollama replaces ':' with '-'
            string ggufFilename = digest.Replace(":", "-");
            string ggufPath = Path.Combine(blobsPath, ggufFilename);

            // Target destination in LM Studio
            string targetDir = Path.Combine(lmStudioRoot, $"{family}-{tag}");
            string targetFile = Path.Combine(targetDir, $"{family}-{tag}.gguf");

            var item = new OllamaModelItem
            {
                Family = family,
                Tag = tag,
                ManifestPath = manifestFilePath,
                DigestHash = digest,
                GgufBlobFilename = ggufFilename,
                GgufBlobPath = ggufPath,
                FileSizeBytes = size > 0 ? size : (File.Exists(ggufPath) ? new FileInfo(ggufPath).Length : 0),
                TargetDirectory = targetDir,
                TargetSymlinkPath = targetFile
            };

            // Check link status
            if (!File.Exists(ggufPath))
            {
                item.Status = ModelLinkStatus.MissingBlob;
                item.StatusMessage = "GGUF Blob Missing";
            }
            else if (File.Exists(targetFile))
            {
                var fileInfo = new FileInfo(targetFile);
                bool isSymlink = fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint);
                item.Status = isSymlink ? ModelLinkStatus.Linked : ModelLinkStatus.Linked;
                item.StatusMessage = isSymlink ? "Symlinked" : "File Exists";
            }
            else
            {
                item.Status = ModelLinkStatus.NotLinked;
                item.StatusMessage = "Not Linked";
            }

            return item;
        }

        /// <summary>
        /// Links selected models to LM Studio using symbolic link or hard link.
        /// </summary>
        public async Task<int> LinkModelsAsync(
            IEnumerable<OllamaModelItem> models,
            AppConfig config,
            IProgress<int>? progress = null,
            CancellationToken cancellationToken = default)
        {
            string lmStudioOllamaRoot = config.GetEffectiveLmStudioOllamaRoot();
            Log($"Ensuring target directory for LM Studio models exists: {lmStudioOllamaRoot}");

            if (!config.DryRun && !Directory.Exists(lmStudioOllamaRoot))
            {
                Directory.CreateDirectory(lmStudioOllamaRoot);
            }

            int successCount = 0;
            var modelList = new List<OllamaModelItem>(models);
            int total = modelList.Count;

            await Task.Run(() =>
            {
                for (int i = 0; i < total; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var model = modelList[i];

                    try
                    {
                        // Ensure target folder exists
                        if (!config.DryRun && !Directory.Exists(model.TargetDirectory))
                        {
                            Log($"Creating LM Studio model directory: {model.TargetDirectory}");
                            Directory.CreateDirectory(model.TargetDirectory);
                        }

                        // Check if link already exists
                        if (File.Exists(model.TargetSymlinkPath))
                        {
                            var fi = new FileInfo(model.TargetSymlinkPath);
                            if (fi.Attributes.HasFlag(FileAttributes.ReparsePoint))
                            {
                                Log($"Skipping {model.FullName}: already linked", "INFO");
                                model.Status = ModelLinkStatus.Linked;
                                model.StatusMessage = "Already Linked";
                                successCount++;
                                progress?.Report((int)(((double)(i + 1) / total) * 100));
                                continue;
                            }
                        }

                        if (config.DryRun)
                        {
                            Log($"[DRY RUN] Would link {model.FullName} ({config.LinkType}) -> {model.TargetSymlinkPath}", "INFO");
                            successCount++;
                            progress?.Report((int)(((double)(i + 1) / total) * 100));
                            continue;
                        }

                        // Execute link creation
                        bool linkSuccess = false;
                        if (config.LinkType == LinkMode.HardLink)
                        {
                            Log($"Creating Hard Link: {model.FullName} -> {model.TargetSymlinkPath}");
                            linkSuccess = CreateHardLink(model.TargetSymlinkPath, model.GgufBlobPath, IntPtr.Zero);
                        }
                        else
                        {
                            Log($"Linking {model.FullName} at {model.TargetSymlinkPath}");
                            // In .NET 7+: File.CreateSymbolicLink(model.TargetSymlinkPath, model.GgufBlobPath);
                            // Win32 API with unprivileged fallback:
                            linkSuccess = CreateSymbolicLink(
                                model.TargetSymlinkPath,
                                model.GgufBlobPath,
                                SYMBOLIC_LINK_FLAG_FILE | SYMBOLIC_LINK_FLAG_ALLOW_UNPRIVILEGED_CREATE
                            );

                            if (!linkSuccess)
                            {
                                // Retry without allow unprivileged flag
                                linkSuccess = CreateSymbolicLink(model.TargetSymlinkPath, model.GgufBlobPath, SYMBOLIC_LINK_FLAG_FILE);
                            }
                        }

                        if (linkSuccess)
                        {
                            model.Status = ModelLinkStatus.Linked;
                            model.StatusMessage = config.LinkType == LinkMode.HardLink ? "Hardlinked" : "Symlinked";
                            Log($"Successfully linked {model.FullName}", "SUCCESS");
                            successCount++;
                        }
                        else
                        {
                            int err = Marshal.GetLastWin32Error();
                            string errMsg = err == 1314 
                                ? "Privilege not held. Please run application as Administrator or enable Windows Developer Mode."
                                : $"Win32 Error code: {err}";
                            Log($"Failed to link {model.FullName}: {errMsg}", "ERROR");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"Exception linking {model.FullName}: {ex.Message}", "ERROR");
                    }

                    progress?.Report((int)(((double)(i + 1) / total) * 100));
                }
            }, cancellationToken);

            return successCount;
        }

        /// <summary>
        /// Safely unlinks selected models from LM Studio by deleting the symlink or hardlink file.
        /// CRITICAL SAFETY GUARANTEE: The original Ollama model blob is never deleted or modified.
        /// </summary>
        public async Task<int> UnlinkModelsAsync(
            IEnumerable<OllamaModelItem> models,
            AppConfig config,
            IProgress<int>? progress = null,
            CancellationToken cancellationToken = default)
        {
            int unlinkedCount = 0;
            var modelList = new List<OllamaModelItem>(models);
            int total = modelList.Count;

            await Task.Run(() =>
            {
                for (int i = 0; i < total; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var model = modelList[i];

                    try
                    {
                        if (File.Exists(model.TargetSymlinkPath))
                        {
                            Log($"Unlinking {model.FullName} from {model.TargetSymlinkPath}");
                            File.Delete(model.TargetSymlinkPath);

                            // Clean up parent directory if empty
                            if (Directory.Exists(model.TargetDirectory))
                            {
                                var entries = Directory.GetFileSystemEntries(model.TargetDirectory);
                                if (entries.Length == 0)
                                {
                                    Directory.Delete(model.TargetDirectory);
                                    Log($"Removed empty model directory: {model.TargetDirectory}");
                                }
                            }

                            model.Status = ModelLinkStatus.NotLinked;
                            model.StatusMessage = "Not Linked";
                            Log($"Successfully unlinked {model.FullName} (Ollama blob preserved).", "SUCCESS");
                            unlinkedCount++;
                        }
                        else
                        {
                            Log($"Model {model.FullName} is not currently linked in LM Studio.", "INFO");
                            model.Status = ModelLinkStatus.NotLinked;
                            model.StatusMessage = "Not Linked";
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"Failed to unlink {model.FullName}: {ex.Message}", "ERROR");
                    }

                    progress?.Report((int)(((double)(i + 1) / total) * 100));
                }
            }, cancellationToken);

            return unlinkedCount;
        }
    }
}
