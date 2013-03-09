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

        public static void ImportXMLItem(string filePath)
        {
            BookReaderContext db = new BookReaderContext();

            XDocument xmlDoc = XDocument.Load(filePath);

            BookCollection bookCollection = new BookCollection
            {            
                Id = Guid.Parse(xmlDoc.Element("bookcollection").Attribute("id").Value),
                Title = (string) xmlDoc.Element("bookcollection").Element("title")
            };

            db.BookCollections.Add(bookCollection);

            var books = from bk in xmlDoc.Descendants("book")
                        select new Book
                        {
                            Id = Guid.Parse(bk.Attribute("id").Value),
                            Title = (string) bk.Element("title"),
                            Author = (string) bk.Element("author"),
                            BookCollectionSequence = Int32.Parse(bk.Element("number").Value),
                            BookCollection = bookCollection,
                            Chapters = (
                                from c in bk.Elements("chapters").Elements("chapter")
                                select new Chapter
                                {
                                    Id = Guid.Parse(c.Attribute("id").Value),
                                    Number = Int32.Parse(c.Element("number").Value),
                                    Title = (string) c.Element("title"),
                                    PreText = (string) c.Element("subtitle"),
                                    Verses = (
                                        from v in c.Elements("verses").Elements("verse")
                                        select new Verse
                                        {
                                            Id = Guid.Parse(v.Attribute("id").Value),
                                            VerseText = (string) v.Element("text"),
                                            VerseNumber = Int32.Parse(v.Element("number").Value)
                                        }
                                    ).ToArray()
                                }).ToArray()
                        };

            foreach (var book in books) {
                db.Books.Add(book);
            }

            db.SaveChanges();
        }

        public static void ConvertToXML(string inputFile, string outputFile)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("bookcollection");
            root.SetAttributeValue("id", Guid.NewGuid());
            

            XElement books = new XElement("books");
            XElement book;

            XElement chapters = new XElement("chapters");
            XElement chapter;

            XElement verses = new XElement("verses");
            XElement verse;

            List<String> fileLines = System.IO.File.ReadAllLines(inputFile).ToList();

            int verseNumber = 1;
            bool isBookCollection = true;
            int bookCollectionSequence = 1;

            foreach (String line in fileLines)
            {
                if (line.StartsWith("!BOOKCOLLECTIONDATA"))
                {
                    string[] collectionData = line.Split('|');

                    if (!String.IsNullOrEmpty(collectionData[1]))
                    {
                        root.SetElementValue("title", collectionData[1]);
                    }

                    isBookCollection = true;
                    
                }
                else if (line.StartsWith("!BOOKDATA"))
                {
                    string[] bookData = line.Split('|');

                    book = new XElement("book");
                    book.SetAttributeValue("id", Guid.NewGuid());
                    if (!String.IsNullOrEmpty(bookData[1]))
                    {
                        book.SetElementValue("title", bookData[1]);
                    }
                    
                    if (!String.IsNullOrEmpty(bookData[2]))
                    {
                        book.SetElementValue("author", bookData[2]);
                    }

                    if (isBookCollection)
                    {
                        book.SetElementValue("number", bookCollectionSequence);
                        bookCollectionSequence++;
                    }

                    books.Add(book);

                    chapters = new XElement("chapters");
                    book.Add(chapters);
                }
                else if (line.StartsWith("!CHAPTERDATA"))
                {

                    string[] chapterData = line.Split('|');
                    chapter = new XElement("chapter");
                    chapter.SetAttributeValue("id", Guid.NewGuid());
                    if (!String.IsNullOrEmpty(chapterData[1]))
                    {
                        chapter.SetElementValue("number", chapterData[1]);
                    }

                    if (!String.IsNullOrEmpty(chapterData[2]))
                    {
                        chapter.SetElementValue("title", chapterData[2]);
                    }

                    if (!String.IsNullOrEmpty(chapterData[3]))
                    {
                        chapter.SetElementValue("subtitle", chapterData[3]);
                    }

                    chapters.Add(chapter);
                    
                    verses = new XElement("verses");
                    chapter.Add(verses);
                    verseNumber = 1;

                }
                else
                {
                    verse = new XElement("verse");
                    verse.SetAttributeValue("id", Guid.NewGuid());

                    if (!String.IsNullOrEmpty(line))
                    {
                        verse.SetElementValue("text", line);
                    }
                    
                    verse.SetElementValue("number", verseNumber++);
                    verses.Add(verse);
                }
            }

            root.Add(books);
            doc.Add(root);
            
            doc.Save(outputFile);
        }

    }
}