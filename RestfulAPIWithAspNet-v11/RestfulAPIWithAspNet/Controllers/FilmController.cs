using RestfulAPIWithAspNet.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Utils.Data;
using RestfulAPIWithAspNet.Conveters;
using RestfulAPIWithAspNet.Data.VO;

namespace RestfulAPIWithAspNet.Controllers
{
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {

        private readonly ILogger _logger;

        private IRepository<Film> _FilmRepository;
        QueryBuilder<Film> queryBuilder = new QueryBuilder<Film>();
        private readonly FilmConverter _converter;

        public FilmController(IRepository<Film> repository, ILogger<FilmController> logger)
        {
            _FilmRepository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<FilmVO> GetAllAsync()
        {
            return _converter.ParseEntityListToVOList(_FilmRepository.GetAll());
        }

        [HttpGet("{id}", Name = "GetFilm")]
        public IActionResult GetByIdAsync(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var film = _FilmRepository.Find(id);
            if (film == null) return this.NotFound();
            return this.Ok(_converter.Parse(film));
        }

        [HttpPost("PagedSearch")]
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Film> pagedSearchDTO)
        {
            string query = queryBuilder.WithDTO(pagedSearchDTO).GetQueryFromDTO("f", "films");

            pagedSearchDTO.List = _FilmRepository.FindWithPagedSearch(query);
            pagedSearchDTO.TotalResults = _FilmRepository.GetCount(queryBuilder.WithDTO(pagedSearchDTO).GetSelectCount("f", "films"));

            return new ObjectResult(pagedSearchDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Film film)
        {
            if (film == null) return BadRequest();
            var returnFilm = _FilmRepository.Add(film);
            return new ObjectResult(_converter.Parse(returnFilm));
        }

        [HttpPut]
        public IActionResult Update([FromBody]Film film)
        {
            var returnFilm = new Film();
            var result = _FilmRepository.Exists(film.Id);
            if (!result) return this.BadRequest();
            film = _FilmRepository.Update(film);
            return new ObjectResult(_converter.Parse(film));
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