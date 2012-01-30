using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookReader.Data.Models;
using BookReader.Data;

namespace BookReader.WcfService
{
    public class BookReaderService : IBookReaderService
    {
        public List<Book> GetBookList()
        {
            return BookManager.GetAll();
        }

        public List<Chapter> GetChapterList(Guid bookId)
        {
            return ChapterManager.GetAllByBook(bookId);
        }

        public List<Verse> GetVerseList(Guid chapterId)
        {
            return VerseManager.GetAllByChapter(chapterId);
        }

    }
}
