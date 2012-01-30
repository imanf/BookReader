using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookReader.Data.Models
{
    [DataContract]
    public abstract class BookLevelType
    {
        public BookLevelType()
        {
            Id = Guid.NewGuid();
        }

        [DataMember]
        [Key]
        public Guid Id { get; set; }
    }
}
