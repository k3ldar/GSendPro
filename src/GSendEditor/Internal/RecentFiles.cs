using System.Text.Json;

using GSendShared;

namespace GSendEditor.Internal
{
    internal sealed class RecentFiles
    {
        private readonly string _recentsFile;

        public RecentFiles()
        {
            _recentsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "GSendPro", "Desktop", "recentfiles.json");
        }

        internal List<RecentFile> GetRecentFiles()
        {
            if (File.Exists(_recentsFile))
            {
                return JsonSerializer.Deserialize<List<RecentFile>>(File.ReadAllText(_recentsFile));
            }

            return new List<RecentFile>();
        }

        internal void AddRecentFile(string fileName, bool isSubprogram)
        {
            List<RecentFile> recentFiles = GetRecentFiles();

            RecentFile savedRecent = recentFiles.FirstOrDefault(r => r.FileName == fileName);

            if (savedRecent != null)
            {
                recentFiles.Remove(savedRecent);
            }

            recentFiles.Insert(0, new RecentFile(fileName, isSubprogram));

            while (recentFiles.Count > Constants.MaximumRecentFiles)
            {
                recentFiles.RemoveAt(recentFiles.Count - 1);
            }

            string json = JsonSerializer.Serialize(recentFiles);
            File.WriteAllText(_recentsFile, json);
        }

        internal void RemoveRecent(RecentFile recent)
        {
            List<RecentFile> recentFiles = GetRecentFiles();

            RecentFile savedRecent = recentFiles.FirstOrDefault(r => r.FileName == recent.FileName);

            if (savedRecent != null)
            {
                recentFiles.Remove(savedRecent);
                string json = JsonSerializer.Serialize(recentFiles);
                File.WriteAllText(_recentsFile, json);
            }
        }
    }
}
