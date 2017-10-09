using RestfulAPIWithAspNet.Models;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Repository
{
    public interface IBookRepository
    {
        Book Add(Book book);
        IEnumerable<Book> GetAll();
        Book Find(string id);
        Book Remove(string id);
        Book Update(Book book);
        bool BookExists(string id);
    }
}