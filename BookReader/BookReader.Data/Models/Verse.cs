using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookReader.Data.Models
{
    [DataContract]
    public class Verse : BookLevelType
    {
        [DataMember]
        public int VerseNumber { get; set; }

        [DataMember]
        public String VerseText { get; set; }

        [DataMember]
        public Guid ChapterId { get; set; }

        public Chapter Chapter { get; set; }

        [InverseProperty("QuotingVerse")]
        public List<Reference> References { get; set; }
    }
}