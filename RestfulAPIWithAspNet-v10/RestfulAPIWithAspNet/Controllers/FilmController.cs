using RestfulAPIWithAspNet.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Utils.Data;

namespace RestfulAPIWithAspNet.Controllers
{
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {

        private readonly ILogger _logger;

        private IRepository<Film> _FilmRepository;
        QueryBuilder<Film> queryBuilder = new QueryBuilder<Film>();

        public FilmController(IRepository<Film> repository, ILogger<FilmController> logger)
        {
            _FilmRepository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Film> GetAllAsync()
        {
            return _FilmRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetFilm")]
        public IActionResult GetByIdAsync(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var film = _FilmRepository.Find(id);
            if (film == null) return this.NotFound();
            return this.Ok(film);
        }

        [HttpPost("PagedSearch")]
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Film> pagedSearchDTO)
        {
            string query = queryBuilder.WithDTO(pagedSearchDTO).GetQueryFromDTO("f", "films");

            pagedSearchDTO.List = _FilmRepository.FindWithPagedSearch(query);
            pagedSearchDTO.TotalResults = _FilmRepository.GetCount(queryBuilder.WithDTO(pagedSearchDTO).GetBaseSelectCount("f", "films"));

            return new ObjectResult(pagedSearchDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Film film)
        {
            if (film == null) return BadRequest();
            var returnFilm = _FilmRepository.Add(film);
            return new ObjectResult(returnFilm);
        }

        [HttpPut]
        public IActionResult Update([FromBody]Film film)
        {
            var returnFilm = new Film();
            var result = _FilmRepository.Exists(film.Id);
            if (!result) return this.BadRequest();
            _FilmRepository.Update(film);
            return new ObjectResult(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var result = _FilmRepository.Exists(id);
            if (!result) return NotFound();
            _FilmRepository.Remove(id);
            return new NoContentResult();
        }
    }
}