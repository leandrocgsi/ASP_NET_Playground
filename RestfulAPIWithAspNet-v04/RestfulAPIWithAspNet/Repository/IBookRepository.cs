using restful_api_with_aspnet.Models;
using System.Collections.Generic;

namespace restful_api_with_aspnet.Repository
{
    public interface IBookRepository
    {
        void Add(Book book);
        IEnumerable<Book> GetAll();
        Book Find(string id);
        Book Remove(string id);
        void Update(Book book);
    }
}