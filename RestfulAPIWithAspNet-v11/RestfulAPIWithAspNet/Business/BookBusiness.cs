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

    public class BookBusiness
    {

        private readonly ILogger _logger;
        private readonly BookConverter _converter;

        private IRepository<Book> _bookRepository;
        private QueryBuilder<Book> _queryBuilder;

        public BookBusiness(IRepository<Book> repository, ILogger<BookBusiness> logger)
        {
            _bookRepository = repository;
            _converter = new BookConverter();
            _logger = logger;
            _queryBuilder = new QueryBuilder<Book>();
        }

        public IEnumerable<BookVO> FindAll()
        {
            var books = _bookRepository.GetAll();
            return _converter.ParseEntityListToVOList(books);
        }

        public BookVO GetByIdAsync(string id)
        {
            var book = _bookRepository.Find(id);
            return _converter.Parse(book);
        }

        public PagedSearchDTO<Book> PagedSearch(PagedSearchDTO<Book> pagedSearchDTO)
        {
            string query = _queryBuilder.WithDTO(pagedSearchDTO).GetQueryFromDTO("b", "books");
          
            pagedSearchDTO.List = _bookRepository.FindWithPagedSearch(query);
            pagedSearchDTO.TotalResults = _bookRepository.GetCount(_queryBuilder.WithDTO(pagedSearchDTO).GetSelectCount("b", "books"));

            return pagedSearchDTO;
        }

        public BookVO Create(Book book)
        {
            if (book == null) return new BookVO();
            var returnBook = _bookRepository.Add(book);
            return _converter.Parse(returnBook);
        }

        public BookVO Update(Book book)
        {
            var returnBook = new Book();
            var result = _bookRepository.Exists(book.Id);
            if (!result) return new BookVO();
            var updated = _bookRepository.Update(book);
            return _converter.Parse(updated);
        }

        public bool Delete(string id)
        {
            if (id == null || "".Equals(id)) return false;
            var result = _bookRepository.Exists(id);
            if (!result) return false;
            _bookRepository.Remove(id);
            return true;
        }
    }
}