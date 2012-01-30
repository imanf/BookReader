using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReader.Data.Models;
using BookReader.ViewModels;
using BookReader.Data;


namespace BookReader.Controllers
{
    public class ReadBookController : Controller
    {
        //
        // GET: /ReadBook/

        public ActionResult Index()
        {
            ViewBag.BookId = new SelectList(BookManager.GetAll(), "Id", "Title");
            
            return View(new ReadBookViewModel());
        }

        [HttpPost]
        public ActionResult Index(ReadBookViewModel readBook)
        {
            ViewBag.BookId = new SelectList(BookManager.GetAll(), "Id", "Title");

            readBook.Book = BookManager.EagerLoadBook(readBook.BookId);
            readBook.References = ReferenceManager.GetReferencesByBook(readBook.BookId);

            return View(readBook);
        }

        public ActionResult ReadReference(Guid id)
        {
            var verse = VerseManager.EagerLoadVerse(id);
            ViewBag.VerseNumber = verse.VerseNumber;

            return PartialView(verse.Chapter.Book);
        }

    }
}
