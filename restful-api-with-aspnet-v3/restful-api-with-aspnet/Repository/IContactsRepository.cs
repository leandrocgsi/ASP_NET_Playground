using restful_api_with_aspnet.Models;
using System.Collections.Generic;

namespace restful_api_with_aspnet.Repository
{
    public interface IContactsRepository
{
    void Add(Contacts item);
    IEnumerable<Contacts> GetAll();
    Contacts Find(string key);
    void Remove(string Id);
    void Update(Contacts item);
}
}
