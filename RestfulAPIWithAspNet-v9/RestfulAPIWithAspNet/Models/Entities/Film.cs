using RestfulAPIWithAspNet.Models.Entities.Base;

namespace RestfulAPIWithAspNet.Models.Entities
{
    public class Film : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int Length { get; set; }
        public string Rating { get; set; }
    }
}
