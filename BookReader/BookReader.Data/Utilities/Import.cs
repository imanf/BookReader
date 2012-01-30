using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using BookReader.Data.Models;

namespace BookReader.Utilities
{
    public class Import
    {
        public static void ImportBook(Book book)
        {
            BookReaderContext db = new BookReaderContext();

            List<String> fileLines = System.IO.File.ReadAllLines(book.FilePath).ToList();
            Guid chapterId = Guid.Empty;
            int verseNumber = 1;

            foreach (String line in fileLines)
            {
                if (line.StartsWith("!CHAPTERDATA"))
                {
                    string[] chapterData = line.Split('|');

                    Chapter chapter = new Chapter
                    {
                        Number = String.IsNullOrEmpty(chapterData[1]) ? (int?)null : Int32.Parse(chapterData[1]),
                        Title = chapterData[2],
                        PreText = chapterData[3],
                        BookId = book.Id
                    };

                    chapterId = chapter.Id;
                    verseNumber = 1;
                    db.Chapters.Add(chapter);
                    db.SaveChanges();
                }
                else
                {
                    Verse verse = new Verse
                    {
                        VerseNumber = verseNumber++,
                        VerseText = line,
                        ChapterId = chapterId
                    };

                    db.Verses.Add(verse);
                }
            }

            db.SaveChanges();
        }
    }
}