using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using UpBrasil.OTP.API.Utils;

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

        internal List<Film> ParseVOListToEntityList(List<FilmVO> Films)
        {
            if (Films == null) return new List<Film>();
            return Films.Select(item => Parse(item)).ToList();
        }

        internal List<FilmVO> ParseEntityListToVOList(List<Film> Films)
        {
            if (Films == null) return new List<FilmVO>();
            return Films.Select(item => Parse(item)).ToList();
        }

        internal IEnumerable<FilmVO> ParseEntityListToVOList(IEnumerable<Film> Films)
        {
            if (Films == null) return new List<FilmVO>();
            return Films.Select(item => Parse(item)).ToList();
        }
    }
}
