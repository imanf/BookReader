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
    public class ChapterController : Controller
    {
        private BookReaderContext db = new BookReaderContext();

        //
        // GET: /Chapter/

        public ViewResult Index()
        {
            var chaptermodels = db.ChapterModels.Include(c => c.Book);
            return View(chaptermodels.ToList());
        }

        //
        // GET: /Chapter/Details/5

        public ViewResult Details(Guid id)
        {
            ChapterModel chaptermodel = db.ChapterModels.Find(id);
            return View(chaptermodel);
        }

        //
        // GET: /Chapter/Create

        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.BookModels, "Id", "Title");
            return View();
        } 

        //
        // POST: /Chapter/Create

        [HttpPost]
        public ActionResult Create(ChapterModel chaptermodel)
        {
            if (ModelState.IsValid)
            {
                chaptermodel.Id = Guid.NewGuid();
                db.ChapterModels.Add(chaptermodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.BookId = new SelectList(db.BookModels, "Id", "Title", chaptermodel.BookId);
            return View(chaptermodel);
        }
        
        //
        // GET: /Chapter/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            ChapterModel chaptermodel = db.ChapterModels.Find(id);
            ViewBag.BookId = new SelectList(db.BookModels, "Id", "Title", chaptermodel.BookId);
            return View(chaptermodel);
        }

        //
        // POST: /Chapter/Edit/5

        [HttpPost]
        public ActionResult Edit(ChapterModel chaptermodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chaptermodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.BookModels, "Id", "Title", chaptermodel.BookId);
            return View(chaptermodel);
        }

        //
        // GET: /Chapter/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            ChapterModel chaptermodel = db.ChapterModels.Find(id);
            return View(chaptermodel);
        }

        //
        // POST: /Chapter/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            ChapterModel chaptermodel = db.ChapterModels.Find(id);
            db.ChapterModels.Remove(chaptermodel);
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