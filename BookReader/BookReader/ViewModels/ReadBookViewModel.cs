using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookReader.Data.Models;

namespace BookReader.ViewModels
{
    public class ReadBookViewModel
    {
        public Guid BookId { get; set; }

        public virtual Book Book { get; set; }

        public ICollection<Reference> References { get; set; }
    }
}