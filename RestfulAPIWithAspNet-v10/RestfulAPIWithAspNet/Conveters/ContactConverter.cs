using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using UpBrasil.OTP.API.Utils;

namespace RestfulAPIWithAspNet.Conveters
{
    public class ContactConverter : IParser<Contact, ContactVO>, IParser<ContactVO, Contact>
    {
        public Contact Parse(ContactVO origin)
        {
            if (origin == null) return new Contact();
            return new Contact
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                AnniversaryDate = origin.AnniversaryDate,
                DateOfBirth = origin.DateOfBirth,
                Email = origin.Email,
                MobilePhone = origin.MobilePhone,
                Company = origin.Company,
                JobTitle = origin.JobTitle,
                IsFamilyMember = origin.IsFamilyMember
            };
        }

        public ContactVO Parse(Contact origin)
        {
            if (origin == null) return new ContactVO();
            return new ContactVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                AnniversaryDate = origin.AnniversaryDate,
                DateOfBirth = origin.DateOfBirth,
                Email = origin.Email,
                MobilePhone = origin.MobilePhone,
                Company = origin.Company,
                JobTitle = origin.JobTitle,
                IsFamilyMember = origin.IsFamilyMember
            };
        }

        internal List<Contact> ParseVOListToEntityList(List<ContactVO> Contacts)
        {
            if (Contacts == null) return new List<Contact>();
            return Contacts.Select(item => Parse(item)).ToList();
        }

        internal List<ContactVO> ParseEntityListToVOList(List<Contact> Contacts)
        {
            if (Contacts == null) return new List<ContactVO>();
            return Contacts.Select(item => Parse(item)).ToList();
        }

        internal IEnumerable<ContactVO> ParseEntityListToVOList(IEnumerable<Contact> Contacts)
        {
            if (Contacts == null) return new List<ContactVO>();
            return Contacts.Select(item => Parse(item)).ToList();
        }
    }
}
