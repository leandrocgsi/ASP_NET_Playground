using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Utils.Converter;
using System.Collections.Generic;
using System.Linq;


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

        public List<Contact> ParseList(List<ContactVO> Contacts)
        {
            if (Contacts == null) return new List<Contact>();
            return Contacts.Select(item => Parse(item)).ToList();
        }

        public List<ContactVO> ParseList(List<Contact> Contacts)
        {
            if (Contacts == null) return new List<ContactVO>();
            return Contacts.Select(item => Parse(item)).ToList();
        }

    }
}
