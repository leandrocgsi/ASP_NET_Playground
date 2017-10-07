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

        private IBookRepository _bookRepository { get; set; }
        private readonly ILogger _logger;

        public BooksController(IBookRepository bookRepository, ILogger<BooksController> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Book> GetAllAsync()
        {
            return _bookRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetByIdAsync(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var book = _bookRepository.Find(id);
            if (book == null) return this.NotFound();
            return this.Ok(book);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Book book)
        {
            if (book == null) return BadRequest();
            var returnBook = _bookRepository.Add(book);
            return new ObjectResult(returnBook);
        }

        [HttpPut]
        public IActionResult Update([FromBody]Book book)
        {
            var returnBook = new Book();
            var result = _bookRepository.BookExists(book.Id);
            if (!result) return this.BadRequest();
            _bookRepository.Update(book);
            return new ObjectResult(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var result = _bookRepository.BookExists(id);
            if (!result) return NotFound();
            _bookRepository.Remove(id);
            return new NoContentResult();
        }
    }
}