using System.Collections.Generic;
using System.Linq;
using RestfulAPIWithAspNet.Models;
using Microsoft.Extensions.Logging;
using System;

namespace RestfulAPIWithAspNet.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ILogger logger;

        private readonly MySQLContext _context;

        public BookRepository(MySQLContext context, ILogger<BookRepository> logger)
        {
            _context = context;
            this.logger = logger;            
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book Find(string id)
        {
            return _context.Books.SingleOrDefault(m => m.Id == id);
        }

        public Book Add(Book book)
        {
            this.logger.LogTrace("Added {0} by {1}", book.Title, book.Author);
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book Update(Book book)
        {
            var result = _context.Books.SingleOrDefault(b => b.Id == book.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                    this.logger.LogTrace("Updated {0} by {1} to {2} by {3}", result.Title, result.Author, book.Title, book.Author);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return result;
        }

        public Book Remove(string id)
        {
            var result = GetBook(id);
            _context.Books.Remove(result);
            _context.SaveChanges();
            return result;
        }

        public bool BookExists(string id)
        {
            return _context.Books.Any(e => e.Id.Equals(id));
        }

        private Book GetBook(string id)
        {
            return _context.Books.SingleOrDefault(b => b.Id == id);
        }
    }
}