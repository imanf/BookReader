using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReader.Models;

namespace BookReader.Controllers
{ 
    public class BookController : Controller
    {
        private BookReaderContext db = new BookReaderContext();

        //
        // GET: /Book/

        public ViewResult Index()
        {
            return View(db.BookModels.ToList());
        }

        //
        // GET: /Book/Details/5

        public ViewResult Details(Guid id)
        {
            BookModel bookmodel = db.BookModels.Find(id);
            return View(bookmodel);
        }

        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Book/Create

        [HttpPost]
        public ActionResult Create(BookModel bookmodel)
        {
            if (ModelState.IsValid)
            {
                bookmodel.Id = Guid.NewGuid();
                db.BookModels.Add(bookmodel);
                db.SaveChanges();
                Utilities.Import.ImportBook(bookmodel.FilePath, bookmodel);
                return RedirectToAction("Index");  
            }

            return View(bookmodel);
        }
        
        //
        // GET: /Book/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            BookModel bookmodel = db.BookModels.Find(id);
            return View(bookmodel);
        }

        //
        // POST: /Book/Edit/5

        [HttpPost]
        public ActionResult Edit(BookModel bookmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookmodel);
        }

        //
        // GET: /Book/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            BookModel bookmodel = db.BookModels.Find(id);
            return View(bookmodel);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            BookModel bookmodel = db.BookModels.Find(id);
            db.BookModels.Remove(bookmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}