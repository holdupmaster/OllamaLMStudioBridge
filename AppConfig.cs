using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OllamaLmStudioBridge
{
    public enum LinkMode
    {
        SymbolicLink,
        HardLink
    }

    public class AppConfig
    {
        public string OllamaModelsPath { get; set; } = string.Empty;
        public string LmStudioModelsPath { get; set; } = string.Empty;
        public string LmStudioSubFolder { get; set; } = "ollama";
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LinkMode LinkType { get; set; } = LinkMode.SymbolicLink;
        
        public bool DryRun { get; set; } = false;
        public bool AutoScanOnStartup { get; set; } = true;
        public bool CleanBrokenLinksOnScan { get; set; } = false;

        private static readonly string ConfigDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "OllamaLmStudioBridge"
        );

        private static readonly string ConfigFilePath = Path.Combine(ConfigDirectory, "settings.json");

        /// <summary>
        /// Detects standard default paths on Windows for Ollama and LM Studio.
        /// </summary>
        public static AppConfig GetDefaultConfig()
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // 1. Check environment variable OLLAMA_MODELS, or fallback to %USERPROFILE%\.ollama\models
            string envOllama = Environment.GetEnvironmentVariable("OLLAMA_MODELS");
            string defaultOllama = !string.IsNullOrWhiteSpace(envOllama) && Directory.Exists(envOllama)
                ? envOllama
                : Path.Combine(userProfile, ".ollama", "models");

            // 2. Default LM Studio path: %USERPROFILE%\.lmstudio\models
            string defaultLmStudio = Path.Combine(userProfile, ".lmstudio", "models");

            return new AppConfig
            {
                OllamaModelsPath = defaultOllama,
                LmStudioModelsPath = defaultLmStudio,
                LmStudioSubFolder = "ollama",
                LinkType = LinkMode.SymbolicLink,
                DryRun = false,
                AutoScanOnStartup = true,
                CleanBrokenLinksOnScan = false
            };
        }

        /// <summary>
        /// Loads settings from disk (%APPDATA%\OllamaLmStudioBridge\settings.json).
        /// If not found, initializes with system defaults.
        /// </summary>
        public static AppConfig Load()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    var config = JsonSerializer.Deserialize<AppConfig>(json);
                    if (config != null)
                    {
                        // Fallback empty fields if upgraded
                        if (string.IsNullOrWhiteSpace(config.OllamaModelsPath) || string.IsNullOrWhiteSpace(config.LmStudioModelsPath))
                        {
                            var defaults = GetDefaultConfig();
                            if (string.IsNullOrWhiteSpace(config.OllamaModelsPath)) config.OllamaModelsPath = defaults.OllamaModelsPath;
                            if (string.IsNullOrWhiteSpace(config.LmStudioModelsPath)) config.LmStudioModelsPath = defaults.LmStudioModelsPath;
                        }
                        return config;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load settings: {ex.Message}");
            }

            var defaultCfg = GetDefaultConfig();
            // Automatically persist default configuration on first run
            try { Save(defaultCfg); } catch { }
            return defaultCfg;
        }

        /// <summary>
        /// Saves current configuration to %APPDATA%\OllamaLmStudioBridge\settings.json
        /// </summary>
        public static void Save(AppConfig config)
        {
            if (!Directory.Exists(ConfigDirectory))
            {
                Directory.CreateDirectory(ConfigDirectory);
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(config, options);
            File.WriteAllText(ConfigFilePath, json);
        }

        public string GetEffectiveLmStudioOllamaRoot()
        {
            return Path.Combine(LmStudioModelsPath, LmStudioSubFolder);
        }
    }
}
