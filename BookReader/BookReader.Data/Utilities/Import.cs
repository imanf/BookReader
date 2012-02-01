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
        public static void ImportItem(string filePath)
        {
            BookReaderContext db = new BookReaderContext();

            BookCollection bookCollection = null;
            Book book = new Book();
            Chapter chapter = new Chapter();

            List<String> fileLines = System.IO.File.ReadAllLines(filePath).ToList();

            int verseNumber = 1;

            foreach (String line in fileLines)
            {
                if (line.StartsWith("!BOOKCOLLECTIONDATA"))
                {
                    string[] collectionData = line.Split('|');

                    bookCollection = new BookCollection
                    {
                        Title = collectionData[1]
                    };

                    db.BookCollections.Add(bookCollection);

                } 
                else if (line.StartsWith("!BOOKDATA"))
                {
                    string[] bookData = line.Split('|');

                    book = new Book
                    {
                        Title = bookData[1],
                        Author = bookData[2],
                        BookCollection = bookCollection
                    };

                    db.Books.Add(book);

                } 
                else if (line.StartsWith("!CHAPTERDATA")) 
                {

                    string[] chapterData = line.Split('|');

                    chapter = new Chapter
                    {
                        Number = String.IsNullOrEmpty(chapterData[1]) ? (int?)null : Int32.Parse(chapterData[1]),
                        Title = chapterData[2],
                        PreText = chapterData[3],
                        Book = book
                    };

                    verseNumber = 1;
                    db.Chapters.Add(chapter);
                }
                else
                {
                    Verse verse = new Verse
                    {
                        VerseNumber = verseNumber++,
                        VerseText = line,
                        Chapter = chapter
                    };

                    db.Verses.Add(verse);
                }
            }

            db.SaveChanges();
        }
    }
}