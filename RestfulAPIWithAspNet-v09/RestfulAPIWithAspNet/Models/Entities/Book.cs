using RestfulAPIWithAspNet.Models.Entities.Base;
using System;
using System.Runtime.Serialization;

namespace RestfulAPIWithAspNet.Models.Entities
{
    [DataContract]
    public class Book : BaseEntity
    {

        [DataMember(Order = 2)]
        public string Title { get; set; }

        [DataMember(Order = 3)]
        public string Author { get; set; }

        [DataMember(Order = 4)]
        public decimal Price { get; set; }

        [DataMember(Order = 5)]
        public DateTime LaunchDate { get; set; } = DateTime.UtcNow;
    }
}