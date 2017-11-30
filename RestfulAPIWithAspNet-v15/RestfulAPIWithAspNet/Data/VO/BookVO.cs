using I18N;
using System;
using System.ComponentModel.DataAnnotations;

namespace RestfulAPIWithAspNet.Data.VO
{
    public class BookVO
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "REQUIRED_FIELD_TITLE_UNDEFINED")]
        public string Title { get; set; }

        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; } = DateTime.UtcNow;
    }
}