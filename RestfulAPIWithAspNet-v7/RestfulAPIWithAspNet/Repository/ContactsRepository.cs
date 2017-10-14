using System.Collections.Generic;
using System.Linq;
using RestfulAPIWithAspNet.Models;
using Microsoft.Extensions.Logging;

namespace RestfulAPIWithAspNet.Repository
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly ILogger logger;

        private readonly MySQLContext _context;

        public ContactsRepository(MySQLContext context, ILogger<ContactsRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }

        public void Add(Contact item)
        {
            _context.Contacts.Add(item);
            _context.SaveChanges();
        }

        public Contact Find(string key)
        {
            return _context.Contacts
                .Where(e => e.MobilePhone.Equals(key))
                .SingleOrDefault();
        }

        public IEnumerable<Contact> GetAll()
        {
            return _context.Contacts;
        }

        public void Remove(string Id)
        {
            var itemToRemove = _context.Contacts.SingleOrDefault(r => r.MobilePhone == Id);
            if (itemToRemove != null)
            _context.Contacts.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public void Update(Contact item)
        {
            var itemToUpdate = _context.Contacts.SingleOrDefault(r => r.MobilePhone == item.MobilePhone);
            if (itemToUpdate != null)
            {
                itemToUpdate.FirstName = item.FirstName;
                itemToUpdate.LastName = item.LastName;
                itemToUpdate.IsFamilyMember = item.IsFamilyMember;
                itemToUpdate.Company = item.Company;
                itemToUpdate.JobTitle = item.JobTitle;
                itemToUpdate.Email = item.Email;
                itemToUpdate.MobilePhone = item.MobilePhone;
                itemToUpdate.DateOfBirth = item.DateOfBirth;
                itemToUpdate.AnniversaryDate = item.AnniversaryDate;
            }
            _context.SaveChanges();
        }
    }
}