using restful_api_with_aspnet.Models;
using restful_api_with_aspnet.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace restful_api_with_aspnet.Controllers
{
    //HACK: See https://www.packtpub.com/application-development/restful-web-services-aspnet-core-video

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

        [HttpPut]
        public IActionResult Update([FromBody]Book book)
        {
            var returnBook = new Book();
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
            return new ObjectResult(result);
        }

        [HttpDelete("{id}")]
        public NoContentResult Delete(string id)
        {
            var result = _context.Books.SingleOrDefault(b => b.Id == id);
            if (result == null)
            {
                //HACK: Remove this mock
                //return this.BadRequest();
            }
            _context.Books.Remove(result);
            return new NoContentResult();
        }

        private bool LivroExists(string id)
        {
            return _context.Books.Any(e => e.Id.Equals(id));
        }
    }
}