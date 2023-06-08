namespace GSendEditor.Internal
{
    internal sealed class RecentFile
    {
        public RecentFile()
        {

        }

        public RecentFile(string fileName, bool isSubprogram)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            FileName = fileName;
            IsSubprogram = isSubprogram;
        }

        public string FileName { get; set; }

        public bool IsSubprogram { get; set; }
    }
}
