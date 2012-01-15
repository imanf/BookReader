using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookReader.Models
{
    public class ReferenceModel
    {
        public Guid Id { get; set; }

        public int StartOffset { get; set; }

        public int EndOffset { get; set; }

        public virtual VerseModel ReferencedVerse { get; set; }
        
        public virtual VerseModel QuotingVerse { get; set; }
    }
}