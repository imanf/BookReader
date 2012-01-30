using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BookReader.Data.Models;


namespace BookReader.Data
{
    public abstract class DataManager<T> where T : BookLevelType
    {
        protected static BookReaderContext db = new BookReaderContext();

        public static List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public static T Get(Guid id)
        {
            return db.Set<T>().Find(id);
        }

        public static void Create(T bookLevel)
        {
            db.Set<T>().Add(bookLevel);
            db.SaveChanges();
        }

        public static void Edit(T bookLevel)
        {
            db.Entry(bookLevel).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void Delete(Guid id)
        {
            T bookLevel = db.Set<T>().Find(id);
            db.Set<T>().Remove(bookLevel);
            db.SaveChanges();
        }
    }

    public class BookManager : DataManager<Book>
    {
        public static Book EagerLoadBook(Guid bookId)
        {
            return db.Books.Include(b => b.Chapters.Select(c => c.Verses)).Where(b => b.Id == bookId).FirstOrDefault();
        }
    }

    public class ChapterManager : DataManager<Chapter>
    {
        public new static List<Chapter> GetAll()
        {
            return db.Chapters.Include(c => c.Book).ToList();
        }

        public static List<Chapter> GetAllByBook(Guid bookId)
        {
            return db.Chapters.Where(c => c.BookId == bookId).ToList();
        }
    }

    public class VerseManager : DataManager<Verse>
    {
        public new static List<Verse> GetAll()
        {
            return db.Verses.Include(v => v.Chapter).ToList();
        }

        public static List<Verse> GetAllByChapter(Guid chapterId)
        {
            return db.Verses.Where(v => v.ChapterId == chapterId).OrderBy(v => v.VerseNumber).ToList();
        }

        public static Verse EagerLoadVerse(Guid verseId)
        {
            return db.Verses.Include(v => v.Chapter).Include(v => v.Chapter.Book).SingleOrDefault(v => v.Id == verseId);
        }

        public static Verse GetByBookIdChapterVerse(Guid bookId, int chapterNumber, int verseNumber)
        {
            var sourceVerse = from verse in db.Verses
                              where verse.Chapter.Book.Id == bookId
                                && verse.Chapter.Number == chapterNumber
                                && verse.VerseNumber == verseNumber
                              select verse;

            return sourceVerse.FirstOrDefault();
        }

    }

    public class ReferenceManager : DataManager<Reference>
    {
        public static List<Reference> GetReferencesByBook(Guid bookId)
        {
            return db.References.Include(x => x.QuotingVerse.Chapter)
                                         .Include(x => x.QuotingVerse.Chapter.Book)
                                         .Where(x => x.ReferencedVerse.Chapter.Book.Id == bookId).ToList();
        }

        public static List<Reference> GetReferencesByVerse(Guid verseId)
        {
            db.Configuration.LazyLoadingEnabled = true;
            return db.References.Where(r => r.QuotingVerse.Id == verseId).Include(a => a.ReferencedVerse).Include(c => c.ReferencedVerse.Chapter).ToList();
        }

        public static void Create(Guid quotingVerseId, Guid referencedVerseId, int startOffset, int endOffset)
        {
            Verse quotingVerse = db.Verses.Find(quotingVerseId);
            Verse referencedVerse = db.Verses.Find(referencedVerseId);
            
            Reference reference = new Reference
            {
                QuotingVerse = quotingVerse,
                ReferencedVerse = referencedVerse,
                StartOffset = startOffset,
                EndOffset = endOffset
            };

            db.References.Add(reference);
            db.SaveChanges();
        }
    }

}
