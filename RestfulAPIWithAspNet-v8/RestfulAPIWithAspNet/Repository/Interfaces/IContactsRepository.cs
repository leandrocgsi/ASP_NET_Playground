using RestfulAPIWithAspNet.Models;
using RestfulAPIWithAspNet.Models.Entities;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Repository.Interfaces
{
    public interface IContactsRepository
    {
        Contact Add(Contact item);
        Contact Update(Contact item);
        void Remove(string Id);
        IEnumerable<Contact> GetAll();
        Contact Find(string key);
    }
}
