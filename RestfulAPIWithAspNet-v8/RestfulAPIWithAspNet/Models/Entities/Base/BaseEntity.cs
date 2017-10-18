using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfulAPIWithAspNet.Models.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }
    }
}