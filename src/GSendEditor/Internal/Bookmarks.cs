using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using FastColoredTextBoxNS;

using GSendShared;

namespace GSendEditor.Internal
{
    internal class Bookmarks
    {
        private readonly string _bookMarksFile;

        public Bookmarks()
        {
            _bookMarksFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder, Constants.GSendProDesktopFolder, "bookmarks.json");
        }

        internal Bookmark GetBookmarkForFile(string fileName)
        {
            if (!String.IsNullOrEmpty(fileName) && File.Exists(_bookMarksFile))
            {
                List<Bookmark> bookmarks = JsonSerializer.Deserialize<List<Bookmark>>(File.ReadAllText(_bookMarksFile));

                if (bookmarks != null)
                {
                    Bookmark bookmark = bookmarks.FirstOrDefault(bm => bm.FileName.Equals(fileName, StringComparison.OrdinalIgnoreCase));

                    if (bookmark != null)
                    {
                        return bookmark;
                    }
                }
            }

            return new Bookmark() { FileName = fileName };
        }

        internal void UpdateBookmarks(Bookmark bookmark)
        {
            if (bookmark == null || String.IsNullOrEmpty(bookmark.FileName))
                return;

            List<Bookmark> bookmarks = null;
            Bookmark foundBookmark = null;

            if (File.Exists(_bookMarksFile))
            {
                bookmarks = JsonSerializer.Deserialize<List<Bookmark>>(File.ReadAllText(_bookMarksFile));

                if (bookmarks != null)
                {
                    foundBookmark = bookmarks.FirstOrDefault(bm => bm.FileName.Equals(bookmark.FileName, StringComparison.OrdinalIgnoreCase));
                }
            }

            if (bookmarks == null)
            {
                bookmarks = new();
            }

            if (foundBookmark != null)
                bookmarks.Remove(foundBookmark);

            bookmarks.Add(bookmark);

            string json = JsonSerializer.Serialize(bookmarks);
            File.WriteAllText(_bookMarksFile, json);
        }
    }
}
