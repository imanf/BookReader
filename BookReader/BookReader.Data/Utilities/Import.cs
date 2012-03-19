using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using BookReader.Data.Models;
using System.Xml.Linq;

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
            bool isBookCollection = false;
            int bookCollectionSequence = 1;

            foreach (String line in fileLines)
            {
                if (line.StartsWith("!BOOKCOLLECTIONDATA"))
                {
                    string[] collectionData = line.Split('|');

                    bookCollection = new BookCollection
                    {
                        Title = collectionData[1]
                    };

                    isBookCollection = true;
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

                    if (isBookCollection)
                    {
                        book.BookCollectionSequence = bookCollectionSequence;
                        bookCollectionSequence++;
                    }

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
                    db.SaveChanges();
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



        public static void ConvertToXML(string inputFile, string outputFile)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("bookcollection");
            

            XElement books = new XElement("books");
            XElement book;

            XElement chapters = new XElement("chapters");
            XElement chapter;

            XElement verses = new XElement("verses");
            XElement verse;

            root.Add(books);

            List<String> fileLines = System.IO.File.ReadAllLines(inputFile).ToList();

            int verseNumber = 1;
            bool isBookCollection = false;
            int bookCollectionSequence = 1;

            foreach (String line in fileLines)
            {
                if (line.StartsWith("!BOOKCOLLECTIONDATA"))
                {
                    string[] collectionData = line.Split('|');

                    root.SetElementValue("name", collectionData[1]);

                    isBookCollection = true;
                    
                }
                else if (line.StartsWith("!BOOKDATA"))
                {
                    string[] bookData = line.Split('|');

                    book = new XElement("book");
                    book.SetAttributeValue("id", Guid.NewGuid());
                    book.SetElementValue("title", bookData[1]);
                    book.SetElementValue("author", bookData[2]);

                    books.Add(book);

                    chapters = new XElement("chapters");
                    book.Add(chapters);
                }
                else if (line.StartsWith("!CHAPTERDATA"))
                {

                    string[] chapterData = line.Split('|');
                    chapter = new XElement("chapter");
                    chapter.SetAttributeValue("id", Guid.NewGuid());
                    chapter.SetElementValue("number", chapterData[1]);
                    chapter.SetElementValue("title", chapterData[2]);
                    chapter.SetElementValue("subtitle", chapterData[3]);

                    chapters.Add(chapter);
                    
                    verses = new XElement("verses");
                    chapter.Add(verses);
                    verseNumber = 1;

                }
                else
                {
                    verse = new XElement("verse");
                    verse.SetAttributeValue("id", Guid.NewGuid());

                    verse.SetElementValue("text", line);
                    verse.SetElementValue("number", verseNumber++);
                    verses.Add(verse);
                }
            }
            doc.Add(root);

            doc.Save(outputFile);
        }

    }
}