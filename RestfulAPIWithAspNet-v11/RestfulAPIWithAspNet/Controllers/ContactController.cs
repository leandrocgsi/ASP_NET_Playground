using RestfulAPIWithAspNet.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Utils.Data;
using RestfulAPIWithAspNet.Conveters;
using RestfulAPIWithAspNet.Data.VO;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Business;

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
        public IEnumerable<ContactVO> GetAll()
        {
            return _business.FindAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var item = _business.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
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
        public void Delete(string id)
        {
            _business.Delete(id);
        }

        [HttpPost("PagedSearch")]
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Contact> pagedSearchDTO)
        {
            return new ObjectResult(_business.PagedSearch(pagedSearchDTO));
        }
    }
}
