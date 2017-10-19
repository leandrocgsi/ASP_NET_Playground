using RestfulAPIWithAspNet.Models.Entities.Base;
using System;

namespace RestfulAPIWithAspNet.Models.Entities
{
    public class Book : BaseEntity
    {

        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; } = DateTime.UtcNow;
    }
}