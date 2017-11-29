using System.ComponentModel.DataAnnotations;

namespace RestfulAPIWithAspNet.Data
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}