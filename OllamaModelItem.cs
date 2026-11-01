using System;
using System.IO;

namespace OllamaLmStudioBridge
{
    public enum ModelLinkStatus
    {
        NotLinked,
        Linked,
        Broken,
        MissingBlob
    }

    public class OllamaModelItem
    {
        public string Family { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string FullName => $"{Family}:{Tag}";
        public string ManifestPath { get; set; } = string.Empty;
        public string DigestHash { get; set; } = string.Empty;
        public string GgufBlobFilename { get; set; } = string.Empty;
        public string GgufBlobPath { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; } = 0;
        public string TargetDirectory { get; set; } = string.Empty;
        public string TargetSymlinkPath { get; set; } = string.Empty;
        public ModelLinkStatus Status { get; set; } = ModelLinkStatus.NotLinked;
        public string StatusMessage { get; set; } = "Not Linked";
        public bool IsSelected { get; set; } = true;

        public string FormattedSize
        {
            get
            {
                if (FileSizeBytes <= 0) return "Unknown";
                double kb = FileSizeBytes / 1024.0;
                double mb = kb / 1024.0;
                double gb = mb / 1024.0;

                if (gb >= 1.0) return $"{gb:F2} GB";
                if (mb >= 1.0) return $"{mb:F1} MB";
                return $"{kb:F0} KB";
            }
        }
    }
}
