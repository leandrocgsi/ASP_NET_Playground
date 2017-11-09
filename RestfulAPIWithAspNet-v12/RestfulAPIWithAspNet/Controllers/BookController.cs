using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Business;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System;

namespace RestfulAPIWithAspNet.Controllers
{
    //HACK: See https://www.packtpub.com/application-development/restful-web-services-aspnet-core-video
    
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {

        private readonly ILogger _logger;

        private BookBusiness _business;

        public BookController(BookBusiness business, ILogger<BookController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerResponse((200), Type = typeof(List<BookVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IEnumerable<BookVO> GetAllAsync()
        {
            return _business.FindAll();
        }

        [HttpGet("{id}")]
        [SwaggerResponse((200), Type = typeof(BookVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult GetByIdAsync(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var book = _business.GetByIdAsync(id);
            if (book == null) return this.NotFound();
            return this.Ok(book);
        }

        [HttpPost("PagedSearch")]
        [SwaggerResponse((200), Type = typeof(PagedSearchDTO<Book>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Book> pagedSearchDTO)
        {
            return new ObjectResult(_business.PagedSearch(pagedSearchDTO));
        }

        [HttpPost]
        [SwaggerResponse((201), Type = typeof(BookVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Create([FromBody]Book book)
        {
            if (book == null) return BadRequest();
            return new ObjectResult(_business.Create(book));
        }

        [HttpPut]
        [SwaggerResponse((202), Type = typeof(BookVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Update([FromBody]Book book)
        {
            var updated = _business.Update(book);
            if (updated.Id == null) return this.BadRequest();
            return new ObjectResult(_business.Update(book));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Delete(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            if (!_business.Delete(id)) return NotFound();
            return new NoContentResult();
        }
    }
}