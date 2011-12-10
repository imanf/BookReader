using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookReader.Models
{
    public class BookModel
    {
        [Key]
        public Guid Id { get; set; }

        public String Title { get; set; }

        public String Author { get; set; }

        public String FilePath { get; set; }

        public virtual ICollection<ChapterModel> Chapters { get; set; }
    }
}