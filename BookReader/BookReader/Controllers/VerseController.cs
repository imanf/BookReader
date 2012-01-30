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
    public class VerseController : Controller
    {

        //
        // GET: /Verse/

        public ViewResult Index()
        {
            return View(VerseManager.GetAll());
        }

        //
        // GET: /Verse/Details/5

        public ViewResult Details(Guid id)
        {
            return View(VerseManager.Get(id));
        }

        //
        // GET: /Verse/Create

        public ActionResult Create()
        {
            ViewBag.ChapterId = new SelectList(ChapterManager.GetAll(), "Id", "Title");
            return View();
        } 

        //
        // POST: /Verse/Create

        [HttpPost]
        public ActionResult Create(Verse verse)
        {
            if (ModelState.IsValid)
            {
                VerseManager.Create(verse);
                return RedirectToAction("Index");  
            }

            return View();
        }
        
        //
        // GET: /Verse/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Verse verse = VerseManager.Get(id);
            ViewBag.ChapterId = new SelectList(ChapterManager.GetAll(), "Id", "Title", verse.ChapterId);
            return View(verse);
        }

        //
        // POST: /Verse/Edit/5

        [HttpPost]
        public ActionResult Edit(Verse verse)
        {
            if (ModelState.IsValid)
            {
                VerseManager.Edit(verse);
                return RedirectToAction("Index");
            }
            
            return View();
        }

        //
        // GET: /Verse/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            return View(VerseManager.Get(id));
        }

        //
        // POST: /Verse/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            VerseManager.Delete(id);
            return RedirectToAction("Index");
        }

    }
}