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
    public class ChapterController : Controller
    {
        //
        // GET: /Chapter/

        public ViewResult Index()
        {
            return View(ChapterManager.GetAll());
        }

        //
        // GET: /Chapter/Details/5

        public ViewResult Details(Guid id)
        {
            return View(ChapterManager.Get(id));
        }

        //
        // GET: /Chapter/Create

        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(BookManager.GetAll(), "Id", "Title");
            return View();
        } 

        //
        // POST: /Chapter/Create

        [HttpPost]
        public ActionResult Create(Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                ChapterManager.Create(chapter);
                return RedirectToAction("Index");  
            }

            return View();
        }
        
        //
        // GET: /Chapter/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Chapter chapter = ChapterManager.Get(id);
            ViewBag.BookId = new SelectList(BookManager.GetAll(), "Id", "Title", chapter.BookId);
            return View(chapter);
        }

        //
        // POST: /Chapter/Edit/5

        [HttpPost]
        public ActionResult Edit(Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                ChapterManager.Edit(chapter);
                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // GET: /Chapter/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Chapter chapter = ChapterManager.Get(id);
            return View(chapter);
        }

        //
        // POST: /Chapter/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            ChapterManager.Delete(id);
            return RedirectToAction("Index");
        }

    }
}