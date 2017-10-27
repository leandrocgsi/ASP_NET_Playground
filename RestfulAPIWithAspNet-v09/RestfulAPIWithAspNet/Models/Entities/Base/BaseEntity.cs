using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace RestfulAPIWithAspNet.Models.Entities.Base
{
    [DataContract]
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        [DataMember(Order = 1)]
        public string Id { get; set; }
    }
}