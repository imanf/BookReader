using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookReader.Models
{
    public class VerseModel
    {
        [Key]
        public Guid Id { get; set; }

        public int VerseNumber { get; set; }

        public String VerseText { get; set; }

        public Guid ChapterId { get; set; }
        public ChapterModel Chapter { get; set; }

        [InverseProperty("QuotingVerse")]
        public List<ReferenceModel> References { get; set; }
    }
}