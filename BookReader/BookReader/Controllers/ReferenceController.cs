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
    [Authorize]
    public class ReferenceController : Controller
    {
        
        public ActionResult LinkVerse()
        {
            ViewBag.Books = new SelectList(BookManager.GetAll(), "Id", "Title");

            return View();
        }

        [HttpPost]
        public ActionResult GetChapters(Guid bookId)
        {
            return Json(new SelectList(ChapterManager.GetAllByBook(bookId), "Id", "Number"));
        }

        [HttpPost]
        public JsonResult GetVerses(Guid chapterId)
        {
            return Json(VerseManager.GetAllByChapter(chapterId));
        }

        [HttpPost]
        public JsonResult GetReferences(Guid verseId)
        {
            var references = ReferenceManager.GetReferencesByVerse(verseId);

            var referencesViewModel = new List<AddReferenceViewModel>();

            foreach (var reference in references)
            {
                referencesViewModel.Add(new AddReferenceViewModel
                {
                    ReferenceId = reference.Id,
                    ChapterName = reference.ReferencedVerse.Chapter.Title,
                    ChapterNumber = reference.ReferencedVerse.Chapter.Number,
                    VerseNumber = reference.ReferencedVerse.VerseNumber
                });
            }

            return Json(referencesViewModel);
        }

        [HttpPost]
        public ActionResult AddReference(Guid targetVerseId, Guid sourceBookId, int sourceChapterNum, int sourceVerseNum, int startOffset, int endOffset)
        {
            var sourceVerse = VerseManager.GetByBookIdChapterVerse(sourceBookId, sourceChapterNum, sourceVerseNum);

            if (startOffset < endOffset)
            {
                ReferenceManager.Create(targetVerseId, sourceVerse.Id, startOffset, endOffset);
            }
            else
            {
                ReferenceManager.Create(targetVerseId, sourceVerse.Id, endOffset, startOffset);
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(Guid referenceId)
        {
            ReferenceManager.Delete(referenceId);
            return View();
        }
    }
}