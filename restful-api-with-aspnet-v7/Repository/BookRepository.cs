using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using restful_api_with_aspnet.Models;

namespace restful_api_with_aspnet.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ConcurrentDictionary<string, Book> books;
        private int nextId = 0;

        //private readonly MySQLContext _context;

        public BookRepository(/*MySQLContext context*/)
        {
            //_context = context;
            this.books = new ConcurrentDictionary<string, Book>();
            this.Add(new Book { Title = "RESTful API with ASP.NET Core MVC 1.0", Author = "Leandro Costa" });
        }

        public void Add(Book book)
        {
            if (book == null)
            {
                return;
            }

            this.nextId++;
            book.Id = nextId.ToString();

            this.books.TryAdd(book.Id, book);
        }

        public Book Find(string id)
        {
            Book book;
            this.books.TryGetValue(id, out book);
            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            return this.books.Values.OrderBy(b => b.Id);
        }

        public Book Remove(string id)
        {
            Book book;
            this.books.TryRemove(id, out book);
            return book;
        }

        public Book Update(Book book)
        {
            //HACK: See https://stackoverflow.com/questions/25894587/how-to-update-record-using-entity-framework-6
            //_context.Books.Attach(book);
            //_context.SaveChanges();
            return book;
        }
    }
}