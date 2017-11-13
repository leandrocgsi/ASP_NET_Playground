using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Data.VO;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Business;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RestfulAPIWithAspNet.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly ILogger _logger;

        private ContactBusiness _business;

        public ContactController(ContactBusiness business, ILogger<ContactController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerResponse((200), Type = typeof(List<ContactVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IEnumerable<ContactVO> GetAll()
        {
            return _business.FindAll();
        }

        [HttpGet("{id}")]
        [SwaggerResponse((200), Type = typeof(ContactVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult GetById(string id)
        {
            var item = _business.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost("PagedSearch")]
        [SwaggerResponse((200), Type = typeof(PagedSearchDTO<Book>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Contact> pagedSearchDTO)
        {
            return new ObjectResult(_business.PagedSearch(pagedSearchDTO));
        }

        [HttpPost]
        [SwaggerResponse((201), Type = typeof(ContactVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Create([FromBody] Contact item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _business.Create(item);
            return new ObjectResult(item);
        }

        [HttpPut]
        [SwaggerResponse((202), Type = typeof(ContactVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Update([FromBody] Contact item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _business.Update(item);
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public void Delete(string id)
        {
            _business.Delete(id);
        }
    }
}
