using RestfulAPIWithAspNet.Models;
using RestfulAPIWithAspNet.Models.Entities;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Repository.Interfaces
{
    public interface IBookRepository
    {
        Book Add(Book book);
        Book Update(Book book);
        Book Remove(string id);
        IEnumerable<Book> GetAll();
        Book Find(string id);
        bool BookExists(string id);
    }
}