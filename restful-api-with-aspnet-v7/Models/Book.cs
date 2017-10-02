using System;

namespace restful_api_with_aspnet.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; } = DateTime.UtcNow;
    }
}