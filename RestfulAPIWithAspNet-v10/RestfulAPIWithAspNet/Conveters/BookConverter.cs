using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpBrasil.OTP.API.Utils;

namespace RestfulAPIWithAspNet.Conveters
{
    public class BookConverter : IParser<Book, BookVO>, IParser<BookVO, Book>
    {
        public Book Parse(BookVO origin)
        {
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
            return new BookVO
            {

            };
        }
    }
}
