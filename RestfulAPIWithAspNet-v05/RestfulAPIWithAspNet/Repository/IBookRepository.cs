using RestfulAPIWithAspNet.Models;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Repository
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