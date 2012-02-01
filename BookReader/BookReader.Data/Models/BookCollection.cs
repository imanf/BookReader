using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookReader.Data.Models
{
    public class BookCollection : BookLevelType
    {
        public string Title { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
