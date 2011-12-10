using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReader.Models
{
    public class SourceBookModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String FilePath { get; set; }
        
    }
}