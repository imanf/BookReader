using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookReader.Data.Models
{
    [DataContract]
    public partial class Chapter : BookLevelType
    {
        [DataMember]
        public int? Number { get; set; }
        
        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String PreText { get; set; }

        [DataMember]
        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public virtual ICollection<Verse> Verses { get; set; }

    }
}