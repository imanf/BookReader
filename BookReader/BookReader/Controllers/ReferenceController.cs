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
    public class ReferenceController : Controller
    {
        private BookReaderContext db = new BookReaderContext();

        /*
        //
        // GET: /Reference/

        public ViewResult Index()
        {
            return View(db.ReferenceModels.ToList());
        }

        //
        // GET: /Reference/Details/5

        public ViewResult Details(Guid id)
        {
            ReferenceModel referencemodel = db.ReferenceModels.Find(id);
            return View(referencemodel);
        }

        //
        // GET: /Reference/Create

        public ActionResult Create()
        {
            //var verses = db.ReferenceModels.Include("Books").Include("Chapters").Include("Verses");
            ViewBag.Books = new SelectList(db.BookModels, "Id", "Title");
            return View();
        } 

        //
        // POST: /Reference/Create

        [HttpPost]
        public ActionResult Create(ReferenceModel referencemodel)
        {
            if (ModelState.IsValid)
            {
                referencemodel.Id = Guid.NewGuid();
                db.ReferenceModels.Add(referencemodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(referencemodel);
        }
        
        //
        // GET: /Reference/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            ReferenceModel referencemodel = db.ReferenceModels.Find(id);
            return View(referencemodel);
        }

        //
        // POST: /Reference/Edit/5

        [HttpPost]
        public ActionResult Edit(ReferenceModel referencemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(referencemodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(referencemodel);
        }

        //
        // GET: /Reference/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            ReferenceModel referencemodel = db.ReferenceModels.Find(id);
            return View(referencemodel);
        }

        //
        // POST: /Reference/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            ReferenceModel referencemodel = db.ReferenceModels.Find(id);
            db.ReferenceModels.Remove(referencemodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        */

        public ActionResult LinkVerse()
        {
            ViewBag.Books = new SelectList(db.BookModels, "Id", "Title");

            return View();
        }

        //[HttpPost]
        //public ActionResult LinkVerse(string chapterId1)
        //{
        //    Guid chapterId = Guid.NewGuid();
        //    var verses = db.VerseModels.Where(c => c.ChapterId == chapterId).OrderBy(n => n.VerseNumber);
        //    ViewBag.Verses = verses;

        //    return View();
        //}

        [HttpPost]
        public ActionResult GetChapters(Guid bookId)
        {
            var chapters = db.ChapterModels.Where(b => b.BookId == bookId);
            var selectList = new SelectList(chapters, "Id", "Number");

            return Json(selectList);
        }

        [HttpPost]
        public JsonResult GetVerses(Guid chapterId)
        {
            var verses = db.VerseModels.Where(c => c.ChapterId == chapterId).OrderBy(n => n.VerseNumber);
            var verses2 = from v in db.VerseModels
                          where v.ChapterId == chapterId
                          from r in db.ReferenceModels
                          where v.Id == r.QuotingVerse.Id
                          orderby v.VerseNumber
                          select v;
            
            return Json(verses);

        }

        [HttpPost]
        public JsonResult GetReferences(Guid verseId)
        {
            db.Configuration.LazyLoadingEnabled = true;

            var references = db.ReferenceModels.Where(r => r.QuotingVerse.Id == verseId).Include(a => a.ReferencedVerse).Include(c => c.ReferencedVerse.Chapter);

            var references2 = new List<AddReferenceViewModel>();

            foreach (var reference in references)
            {
                references2.Add(new AddReferenceViewModel
                {
                    ReferenceId = reference.Id,
                    ChapterName = reference.ReferencedVerse.Chapter.Title,
                    ChapterNumber = reference.ReferencedVerse.Chapter.Number,
                    VerseNumber = reference.ReferencedVerse.VerseNumber
                });
            }

            return Json(references2);
        }

        [HttpPost]
        public ActionResult AddReference(Guid targetVerseId, Guid sourceBookId, int sourceChapterNum, int sourceVerseNum, int startOffset, int endOffset)
        {
            var targetVerse = db.VerseModels.Find(targetVerseId);
            
            var sourceVerse = from verse in db.VerseModels
                              where verse.Chapter.Book.Id == sourceBookId
                                && verse.Chapter.Number == sourceChapterNum
                                && verse.VerseNumber == sourceVerseNum
                              select verse;


            ReferenceModel reference = new ReferenceModel
            {
                Id = Guid.NewGuid(),
                QuotingVerse = targetVerse,
                ReferencedVerse = sourceVerse.Single(),
                StartOffset = startOffset,
                EndOffset = endOffset
            };

            if (startOffset > endOffset)
            {
                reference.StartOffset = endOffset;
                reference.EndOffset = startOffset;
            }

            db.ReferenceModels.Add(reference);
            db.SaveChanges();

            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid referenceId)
        {
            var reference = db.ReferenceModels.Find(referenceId);

            db.ReferenceModels.Remove(reference);
            db.SaveChanges();

            return View();
        }
    }
}