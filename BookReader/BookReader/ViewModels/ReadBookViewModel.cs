using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookReader.Models;

namespace BookReader.ViewModels
{
    public class ReadBookViewModel
    {
        public Guid BookId { get; set; }

        public virtual BookModel Book { get; set; }

        public ICollection<ReferenceModel> References { get; set; }
    }
}