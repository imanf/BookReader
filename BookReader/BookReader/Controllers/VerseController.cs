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
    public class VerseController : Controller
    {
        private BookReaderContext db = new BookReaderContext();

        //
        // GET: /Verse/

        public ViewResult Index()
        {
            var versemodels = db.VerseModels.Include(v => v.Chapter);
            return View(versemodels.ToList());
        }

        //
        // GET: /Verse/Details/5

        public ViewResult Details(Guid id)
        {
            VerseModel versemodel = db.VerseModels.Find(id);
            return View(versemodel);
        }

        //
        // GET: /Verse/Create

        public ActionResult Create()
        {
            ViewBag.ChapterId = new SelectList(db.ChapterModels, "Id", "Title");
            return View();
        } 

        //
        // POST: /Verse/Create

        [HttpPost]
        public ActionResult Create(VerseModel versemodel)
        {
            if (ModelState.IsValid)
            {
                versemodel.Id = Guid.NewGuid();
                db.VerseModels.Add(versemodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ChapterId = new SelectList(db.ChapterModels, "Id", "Title", versemodel.ChapterId);
            return View(versemodel);
        }
        
        //
        // GET: /Verse/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            VerseModel versemodel = db.VerseModels.Find(id);
            ViewBag.ChapterId = new SelectList(db.ChapterModels, "Id", "Title", versemodel.ChapterId);
            return View(versemodel);
        }

        //
        // POST: /Verse/Edit/5

        [HttpPost]
        public ActionResult Edit(VerseModel versemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(versemodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChapterId = new SelectList(db.ChapterModels, "Id", "Title", versemodel.ChapterId);
            return View(versemodel);
        }

        //
        // GET: /Verse/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            VerseModel versemodel = db.VerseModels.Find(id);
            return View(versemodel);
        }

        //
        // POST: /Verse/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            VerseModel versemodel = db.VerseModels.Find(id);
            db.VerseModels.Remove(versemodel);
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