using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Business;

namespace RestfulAPIWithAspNet.Controllers
{
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {

        private readonly ILogger _logger;

        private FilmBusiness _business;

        public FilmController(FilmBusiness business, ILogger<FilmController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<FilmVO> GetAllAsync()
        {
            return _business.FindAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdAsync(string id)
        {
            var item = _business.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost("PagedSearch")]
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Film> pagedSearchDTO)
        {
            return new ObjectResult(_business.PagedSearch(pagedSearchDTO));
        }

        [HttpPost]
        public IActionResult Create([FromBody]Film item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _business.Create(item);
            return new ObjectResult(item);
        }

        [HttpPut]
        public IActionResult Update([FromBody]Film item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _business.Create(item);
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _business.Delete(id);
            return new NoContentResult();
        }
    }
}