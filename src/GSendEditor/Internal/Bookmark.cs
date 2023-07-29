namespace GSendEditor.Internal
{
    internal class Bookmark
    {
        public string FileName { get; set; }

        public List<int> Lines { get; set; } = new();
    }
}
