﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Business;
using Swashbuckle.AspNetCore.SwaggerGen;
using RestfulAPIWithAspNet.HATEOAS;

namespace RestfulAPIWithAspNet.Controllers
{
    //HACK: See https://www.packtpub.com/application-development/restful-web-services-aspnet-core-video
    
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {

        private BookBusiness _business;
        private HATEOASHelper _HATEOASHelper;

        public BookController(BookBusiness business, HATEOASHelper hateoasHelper)
        {
            _business = business;
            _HATEOASHelper = hateoasHelper;
        }

        [HttpGet]
        [SwaggerResponse((200), Type = typeof(List<BookVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IEnumerable<BookVO> GetAll()
        {
            return _business.FindAll();
        }

        [HttpGet("{id}")]
        [SwaggerResponse((200), Type = typeof(BookVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult GetById(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var item = _business.GetByIdAsync(id);
            if (item == null) return this.NotFound();
           // item.AddLinks(_HATEOASHelper.CreateLinks(item));
            return this.Ok(item);
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
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromBody]BookVO item)
        {
            if (!ModelState.IsValid || item == null) return BadRequest();
            return new ObjectResult(_business.Create(item));
        }

        [HttpPut]
        [SwaggerResponse((202), Type = typeof(BookVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Update([FromBody]Book item)
        {
            var updated = _business.Update(item);
            if (updated.Id == null) return this.BadRequest();
            return new ObjectResult(_business.Update(item));
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