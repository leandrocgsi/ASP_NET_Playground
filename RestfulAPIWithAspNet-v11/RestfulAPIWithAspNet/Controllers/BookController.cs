using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Conveters;
using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Business;

namespace RestfulAPIWithAspNet.Controllers
{
    //HACK: See https://www.packtpub.com/application-development/restful-web-services-aspnet-core-video
    //SEE: https://github.com/bragil/diario-bordo and https://bragil.wordpress.com/2012/12/13/dao-generico-entity-framework-5-code-first/

    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {

        private readonly ILogger _logger;

        private BookBusiness _bookBusiness;

        public BookController(BookBusiness bookBusiness, ILogger<BookController> logger)
        {
            _bookBusiness = bookBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<BookVO> GetAllAsync()
        {
            return _bookBusiness.FindAll();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetByIdAsync(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var book = _bookBusiness.GetByIdAsync(id);
            if (book == null) return this.NotFound();
            return this.Ok(book);
        }

        [HttpPost("PagedSearch")]
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Book> pagedSearchDTO)
        {
            return new ObjectResult(_bookBusiness.PagedSearch(pagedSearchDTO));
        }

        [HttpPost]
        public IActionResult Create([FromBody]Book book)
        {
            if (book == null) return BadRequest();
            return new ObjectResult(_bookBusiness.Create(book));
        }

        [HttpPut]
        public IActionResult Update([FromBody]Book book)
        {
            var updated = _bookBusiness.Update(book);
            if (updated.Id == null) return this.BadRequest();
            return new ObjectResult(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            if (!_bookBusiness.Delete(id)) return NotFound();
            return new NoContentResult();
        }
    }
}