using RestfulAPIWithAspNet.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;

namespace RestfulAPIWithAspNet.Controllers
{
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {

        private readonly ILogger _logger;

        private IRepository<Film> _filmRepository;

        public FilmController(IRepository<Film> repository, ILogger<FilmController> logger)
        {
            _filmRepository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Film> GetAllAsync()
        {
            return _filmRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetFilm")]
        public IActionResult GetByIdAsync(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var film = _filmRepository.Find(id);
            if (film == null) return this.NotFound();
            return this.Ok(film);
        }

        [HttpPost("PagedSearch")]
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Film> pagedSearchDTO)
        {

            return new ObjectResult(pagedSearchDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Film film)
        {
            if (film == null) return BadRequest();
            var returnFilm = _filmRepository.Add(film);
            return new ObjectResult(returnFilm);
        }

        [HttpPut]
        public IActionResult Update([FromBody]Film film)
        {
            var returnFilm = new Film();
            var result = _filmRepository.Exists(film.Id);
            if (!result) return this.BadRequest();
            _filmRepository.Update(film);
            return new ObjectResult(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var result = _filmRepository.Exists(id);
            if (!result) return NotFound();
            _filmRepository.Remove(id);
            return new NoContentResult();
        }
    }
}