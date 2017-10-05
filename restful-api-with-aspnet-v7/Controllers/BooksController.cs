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

        private readonly IBookRepository bookRepository;
        private readonly ILogger logger;

        public BooksController(MySQLContext context, IBookRepository bookRepository, ILogger<BooksController> logger)
        {
            _context = context;
            this.bookRepository = bookRepository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            if (id == null || "".Equals(id))
            {
                return NotFound();
            }
            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
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
            return new ObjectResult(returnBook);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]Book book)
        {
            //if (book.Id != id)
            //{
            //    return this.BadRequest();
            //}

            var existingBook = _context.Books.Find(book.Id);
            //if (existingBook == null)
            //{
            //    return this.NotFound();
            //}

            var returnBook = bookRepository.Update(book);

            this.logger.LogTrace(
                "Updated {0} by {1} to {2} by {3}",
                existingBook.Title,
                existingBook.Author,
                book.Title,
                book.Author);

            return new ObjectResult(returnBook);
        }

        [HttpDelete("{id}")]
        public NoContentResult Delete(string id)
        {
            this.bookRepository.Remove(id);
            return new NoContentResult();
        }

        private bool LivroExists(string id)
        {
            return _context.Books.Any(e => e.Id.Equals(id));
        }
    }
}