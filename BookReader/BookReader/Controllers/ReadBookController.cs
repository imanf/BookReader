using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReader.Models;
using BookReader.ViewModels;


namespace BookReader.Controllers
{
    public class ReadBookController : Controller
    {
        private BookReaderContext db = new BookReaderContext();
        
        //
        // GET: /ReadBook/

        public ActionResult Index()
        {
            ViewBag.BookId = new SelectList(db.BookModels, "Id", "Title");
            
            return View(new ReadBookViewModel());
        }

        [HttpPost]
        public ActionResult Index(ReadBookViewModel readBook)
        {
            ViewBag.BookId = new SelectList(db.BookModels, "Id", "Title");

            readBook.Book = db.BookModels.Find(readBook.BookId);
            readBook.References = db.ReferenceModels.Include(x => x.QuotingVerse.Chapter)
                                                    .Include(x => x.QuotingVerse.Chapter.Book)
                                                    .Where(x => x.ReferencedVerse.Chapter.Book.Id == readBook.Book.Id).ToList();

            return View(readBook);
        }

        public ActionResult ReadReference(Guid id)
        {
            var verse = db.VerseModels.Include(x => x.Chapter).Include(x => x.Chapter.Book).SingleOrDefault(x => x.Id == id);
            var book = verse.Chapter.Book;
            ViewBag.VerseNumber = verse.VerseNumber;

            return PartialView(book);
        }

    }
}
