using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReader.Data.Models;
using BookReader.Data;

namespace BookReader.Controllers
{ 
    public class BookController : Controller
    {
        //
        // GET: /Book/

        public ViewResult Index()
        {
            return View(BookManager.GetAll());
        }

        //
        // GET: /Book/Details/5

        public ViewResult Details(Guid id)
        {
            return View(BookManager.Get(id));
        }

        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            ViewBag.BookCollection = new SelectList(BookCollectionManager.GetAll(), "Id", "Title");
            return View();
        } 

        //
        // POST: /Book/Create

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                BookManager.Create(book);
                //TODO: Create FilePath or file upload field
                Utilities.Import.ImportItem("");
                return RedirectToAction("Index");  
            }

            return View(book);
        }
        
        //
        // GET: /Book/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Book book = BookManager.Get(id);
            ViewBag.BookCollection = new SelectList(BookCollectionManager.GetAll(), "Id", "Title", book.BookCollection.Id);
            return View(book);
        }

        //
        // POST: /Book/Edit/5

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            
            // HACK allows us to set the book collection to empty
            if (Request.Form["BookCollection.Id"] == String.Empty)
            {
                ViewBag.BookCollection = new SelectList(BookCollectionManager.GetAll(), "Id", "Title", book.BookCollection.Id);
                book.BookCollection = null;
                ModelState.Remove("BookCollection.Id");
            }

            if (ModelState.IsValid)
            {
                BookManager.Edit(book);
                return RedirectToAction("Index");
            }

            return View(book);
        }

        //
        // GET: /Book/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            return View(BookManager.Get(id));
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            BookManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}