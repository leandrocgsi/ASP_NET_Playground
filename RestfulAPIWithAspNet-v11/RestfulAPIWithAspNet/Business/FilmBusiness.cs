using RestfulAPIWithAspNet.Repository;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Utils.Data;
using RestfulAPIWithAspNet.Conveters;
using RestfulAPIWithAspNet.Data.VO;

namespace RestfulAPIWithAspNet.Business
{

    public class FilmBusiness
    {

        private readonly ILogger _logger;
        private readonly FilmConverter _converter;

        private IRepository<Film> _filmRepository;
        private QueryBuilder<Film> _queryBuilder;

        public FilmBusiness(IRepository<Film> repository, ILogger<FilmBusiness> logger)
        {
            _filmRepository = repository;
            _converter = new FilmConverter();
            _logger = logger;
            _queryBuilder = new QueryBuilder<Film>();
        }

        public IEnumerable<FilmVO> FindAll()
        {
            var films = _filmRepository.GetAll();
            return _converter.ParseList(films);
        }

        public FilmVO GetByIdAsync(string id)
        {
            var film = _filmRepository.Find(id);
            return _converter.Parse(film);
        }

        public PagedSearchDTO<Film> PagedSearch(PagedSearchDTO<Film> pagedSearchDTO)
        {
            string query = _queryBuilder.WithDTO(pagedSearchDTO).GetQueryFromDTO("b", "films");
          
            pagedSearchDTO.List = _filmRepository.FindWithPagedSearch(query);
            pagedSearchDTO.TotalResults = _filmRepository.GetCount(_queryBuilder.WithDTO(pagedSearchDTO).GetSelectCount("b", "films"));

            return pagedSearchDTO;
        }

        public FilmVO Create(Film film)
        {
            if (film == null) return new FilmVO();
            var returnFilm = _filmRepository.Add(film);
            return _converter.Parse(returnFilm);
        }

        public FilmVO Update(Film film)
        {
            var returnFilm = new Film();
            var result = _filmRepository.Exists(film.Id);
            if (!result) return new FilmVO();
            var updated = _filmRepository.Update(film);
            return _converter.Parse(updated);
        }

        public bool Delete(string id)
        {
            if (id == null || "".Equals(id)) return false;
            var result = _filmRepository.Exists(id);
            if (!result) return false;
            _filmRepository.Remove(id);
            return true;
        }
    }
}