using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using BookReader.Models;
using System.Web.Mvc;

namespace BookReader.Utilities
{
    public class Import : Controller
    {
        public static void ImportBook(String bookPath, BookModel book)
        {
            BookReaderContext db = new BookReaderContext();

            List<String> fileLines = System.IO.File.ReadAllLines(bookPath).ToList();
            Guid chapterId = Guid.Empty;
            int verseNumber = 1;

            foreach (String line in fileLines)
            {
                if (line.StartsWith("!CHAPTERDATA"))
                {
                    string[] chapterData = line.Split('|');

                    ChapterModel chapter = new ChapterModel
                    {
                        Id = Guid.NewGuid(),
                        Number = String.IsNullOrEmpty(chapterData[1]) ? (int?)null : Int32.Parse(chapterData[1]),
                        Title = chapterData[2],
                        PreText = chapterData[3],
                        BookId = book.Id
                    };

                    chapterId = chapter.Id;
                    verseNumber = 1;
                    db.ChapterModels.Add(chapter);
                    db.SaveChanges();
                }
                else
                {
                    VerseModel verse = new VerseModel
                    {
                        Id = Guid.NewGuid(),
                        VerseNumber = verseNumber++,
                        VerseText = line,
                        ChapterId = chapterId
                    };

                    db.VerseModels.Add(verse);
                }
            }

            db.SaveChanges();
        }
    }
}