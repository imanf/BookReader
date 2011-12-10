using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookReader.Models
{
    public partial class ChapterModel
    {
        [Key]
        public Guid Id { get; set; }

        public int? Number { get; set; }
        
        public String Title { get; set; }

        public String PreText { get; set; }

        public Guid BookId { get; set; }
        public BookModel Book { get; set; }

        public virtual ICollection<VerseModel> Verses { get; set; }

    }
}