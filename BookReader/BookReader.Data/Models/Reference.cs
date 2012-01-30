using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookReader.Data.Models
{
    public class Reference : BookLevelType
    {
        public int StartOffset { get; set; }

        public int EndOffset { get; set; }

        public  Verse ReferencedVerse { get; set; }

        //public Guid ReferencedVerseId { get; set; }
        
        public  Verse QuotingVerse { get; set; }

        //public Guid QuotingVerseId { get; set; }
    }
}