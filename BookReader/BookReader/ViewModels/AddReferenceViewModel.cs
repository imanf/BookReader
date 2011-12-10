using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookReader.ViewModels
{
    public class AddReferenceViewModel 
    {
        public Guid ReferenceId { get; set; }
        public string BookName { get; set; }
        public string ChapterName { get; set; }
        public int? ChapterNumber { get; set; }
        public int VerseNumber { get; set; }
    }
}
