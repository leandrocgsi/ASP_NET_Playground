using System.Collections.Generic;
using System.Linq;
using RestfulAPIWithAspNet.Models;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Repository.Interfaces;
using System;
using RestfulAPIWithAspNet.Models.Entities;

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

        public IEnumerable<Contact> GetAll()
        {
            return _context.Contacts.ToList();
        }

        public Contact Find(string key)
        {
            return _context.Contacts
                .Where(e => e.Id.Equals(key))
                .SingleOrDefault();
        }

        public Contact Add(Contact item)
        {
            _context.Contacts.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Contact Update(Contact item)
        {
            var result = _context.Contacts.SingleOrDefault(c => c.Id == item.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    this.logger.LogTrace("Updated {0} by {1} to {2} by {3}", result.FirstName, result.LastName, item.FirstName, item.LastName);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return result;
        }

        public void Remove(string Id)
        {
            var itemToRemove = GetContact(Id);
            if (itemToRemove != null) _context.Contacts.Remove(itemToRemove);
            _context.SaveChanges();
        }

        public bool BookExists(string id)
        {
            return _context.Contacts.Any(e => e.Id.Equals(id));
        }

        private Contact GetContact(string Id)
        {
            return _context.Contacts.SingleOrDefault(c => c.Id == Id);
        }
    }
}