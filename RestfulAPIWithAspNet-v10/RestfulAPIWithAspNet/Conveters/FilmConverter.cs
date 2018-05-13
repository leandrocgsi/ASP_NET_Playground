using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Utils.Converter;
using System.Collections.Generic;
using System.Linq;

namespace RestfulAPIWithAspNet.Conveters
{
    public class FilmConverter : IParser<Film, FilmVO>, IParser<FilmVO, Film>
    {
        public Film Parse(FilmVO origin)
        {
            if (origin == null) return new Film();
            return new Film
            {
                Id = origin.Id,
                Title = origin.Title,
                Description = origin.Description,
                ReleaseYear = origin.ReleaseYear,
                Length = origin.Length,
                Rating = origin.Rating
            };
        }

        public FilmVO Parse(Film origin)
        {
            if (origin == null) return new FilmVO();
            return new FilmVO
            {
                Id = origin.Id,
                Title = origin.Title,
                Description = origin.Description,
                ReleaseYear = origin.ReleaseYear,
                Length = origin.Length,
                Rating = origin.Rating
            };
        }

        public List<Film> ParseList(List<FilmVO> Films)
        {
            if (Films == null) return new List<Film>();
            return Films.Select(item => Parse(item)).ToList();
        }

        public List<FilmVO> ParseList(List<Film> Films)
        {
            if (Films == null) return new List<FilmVO>();
            return Films.Select(item => Parse(item)).ToList();
        }

    }
}
