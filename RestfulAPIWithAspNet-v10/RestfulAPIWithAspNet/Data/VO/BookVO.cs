using System;

namespace RestfulAPIWithAspNet.Data.VO
{
    public class BookVO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; } = DateTime.UtcNow;
    }
}