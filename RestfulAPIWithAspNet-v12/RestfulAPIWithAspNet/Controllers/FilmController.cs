using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Business;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RestfulAPIWithAspNet.Controllers
{
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {

        private FilmBusiness _business;

        public FilmController(FilmBusiness business, ILogger<FilmController> logger)
        {
            _business = business;
        }

        [HttpGet]
        [SwaggerResponse((200), Type = typeof(List<FilmVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IEnumerable<FilmVO> GetAll()
        {
            return _business.FindAll();
        }

        [HttpGet("{id}")]
        [SwaggerResponse((200), Type = typeof(FilmVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult GetById(string id)
        {
			if (id == null || "".Equals(id)) return BadRequest();
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
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Film> pagedSearchDTO)
        {
            return new ObjectResult(_business.PagedSearch(pagedSearchDTO));
        }

        [HttpPost]
        [SwaggerResponse((201), Type = typeof(FilmVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
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
        [SwaggerResponse((202), Type = typeof(FilmVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        public IActionResult Update([FromBody]Film item)
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
        public IActionResult Delete(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            if (!_business.Delete(id)) return NotFound();
            _business.Delete(id);
            return new NoContentResult();
        }
    }
}