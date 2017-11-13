using RestfulAPIWithAspNet.Models.Entities.Base;
using System;
using System.Runtime.Serialization;

namespace RestfulAPIWithAspNet.Models.Entities
{
    public class Contact : BaseEntity
    {
        [DataMember(Order = 2)]
        public string FirstName { get; set; }

        [DataMember(Order = 3)]
        public string LastName { get; set; }

        [DataMember(Order = 4)]
        public bool IsFamilyMember { get; set; }

        [DataMember(Order = 5)]
        public string Company { get; set; }

        [DataMember(Order = 6)]
        public string JobTitle { get; set; }

        [DataMember(Order = 7)]
        public string Email { get; set; }

        [DataMember(Order = 8)]
        public string MobilePhone { get; set; }

        [DataMember(Order = 9)]
        public DateTime DateOfBirth { get; set; }

        [DataMember(Order = 10)]
        public DateTime AnniversaryDate { get; set; }
    }
}