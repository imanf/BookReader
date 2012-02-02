using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookReader.Data.Models
{
    [DataContract]
    public class Book : BookLevelType
    {
        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String Author { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }

        public BookCollection BookCollection { get; set; }

        public int BookCollectionSequence { get; set; }

    }
}