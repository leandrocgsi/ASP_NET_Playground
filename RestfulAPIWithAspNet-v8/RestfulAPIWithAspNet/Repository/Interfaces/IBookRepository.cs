using RestfulAPIWithAspNet.Models;
using RestfulAPIWithAspNet.Models.Entities;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Repository.Interfaces
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