using RestfulAPIWithAspNet.Models;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Repository
{
    public interface IContactsRepository
    {
        void Add(Contact item);
        IEnumerable<Contact> GetAll();
        Contact Find(string key);
        void Remove(string Id);
        void Update(Contact item);
    }
}
