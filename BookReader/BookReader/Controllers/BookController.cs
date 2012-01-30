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
                Utilities.Import.ImportBook(book);
                return RedirectToAction("Index");  
            }

            return View(book);
        }
        
        //
        // GET: /Book/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            return View(BookManager.Get(id));
        }

        //
        // POST: /Book/Edit/5

        [HttpPost]
        public ActionResult Edit(Book book)
        {
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