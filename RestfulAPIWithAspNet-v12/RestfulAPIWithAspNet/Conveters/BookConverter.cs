using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using UpBrasil.OTP.API.Utils;

namespace RestfulAPIWithAspNet.Conveters
{
    public class BookConverter : IParser<Book, BookVO>, IParser<BookVO, Book>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null) return new Book();
            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public BookVO Parse(Book origin)
        {
            if (origin == null) return new BookVO();
            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public List<Book> ParseList(List<BookVO> Books)
        {
            if (Books == null) return new List<Book>();
            return Books.Select(item => Parse(item)).ToList();
        }

        public List<BookVO> ParseList(List<Book> Books)
        {
            if (Books == null) return new List<BookVO>();
            return Books.Select(item => Parse(item)).ToList();
        }

    }
}
