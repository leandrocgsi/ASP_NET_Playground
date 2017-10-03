using restful_api_with_aspnet.Models;
using restful_api_with_aspnet.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace restful_api_with_aspnet.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly MySQLContext _context;

        private readonly IBookRepository books;
        private readonly ILogger logger;

        public BooksController(MySQLContext context, IBookRepository books, ILogger<BooksController> logger)
        {
            _context = context;
            this.books = books;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetById(string id)
        {
            var book = this.books.Find(id);
            if (book == null)
            {
                return this.NotFound();
            }

            return this.Ok(book);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Book book)
        {
            if (book == null)
            {
                return this.BadRequest();
            }

            this.logger.LogTrace("Added {0} by {1}", book.Title, book.Author);

            _context.Books.Add(book);
            var returnBook = _context.SaveChanges();
            //return returnBook;
            return new NoContentResult();
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]Book book)
        {
            if (book.Id != id)
            {
                return this.BadRequest();
            }

            var existingBook = this.books.Find(id);
            if (existingBook == null)
            {
                return this.NotFound();
            }

            this.books.Update(book);

            this.logger.LogTrace(
                "Updated {0} by {1} to {2} by {3}",
                existingBook.Title,
                existingBook.Author,
                book.Title,
                book.Author);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public NoContentResult Delete(string id)
        {
            this.books.Remove(id);
            return new NoContentResult();
        }
    }
}