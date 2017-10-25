using RestfulAPIWithAspNet.Models.Entities.Base;
using System.Runtime.Serialization;

namespace RestfulAPIWithAspNet.Models.Entities
{
    [DataContract]
    public class Film : BaseEntity
    {
        [DataMember(Order = 2)]
        public string Title { get; set; }

        [DataMember(Order = 3)]
        public string Description { get; set; }

        [DataMember(Order = 4)]
        public int ReleaseYear { get; set; }

        [DataMember(Order = 5)]
        public int Length { get; set; }

        [DataMember(Order = 6)]
        public string Rating { get; set; }
    }
}
