using RestfulAPIWithAspNet.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulAPIWithAspNet.Data.DTO;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Utils.Data;

namespace RestfulAPIWithAspNet.Controllers
{
    //HACK: See https://www.packtpub.com/application-development/restful-web-services-aspnet-core-video
    //SEE: https://github.com/bragil/diario-bordo and https://bragil.wordpress.com/2012/12/13/dao-generico-entity-framework-5-code-first/

    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {

        private readonly ILogger _logger;

        private IRepository<Book> _bookRepository;
        QueryBuilder<Book> queryBuilder = new QueryBuilder<Book>();

        public BookController(IRepository<Book> repository, ILogger<BookController> logger)
        {
            _bookRepository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Book> GetAllAsync()
        {
            return _bookRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetByIdAsync(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var book = _bookRepository.Find(id);
            if (book == null) return this.NotFound();
            return this.Ok(book);
        }

        [HttpPost("PagedSearch")]
        public IActionResult PagedSearch([FromBody] PagedSearchDTO<Book> pagedSearchDTO)
        {
            if (pagedSearchDTO == null)
            {
                //FAIL
            }

            //HACK: Convert request response
            string query = queryBuilder.WithDTO(pagedSearchDTO).GetQueryFromDTOWithColumnAlias("b", "book");
            object[] parameters = { };//paramsGetterHelper.WithDTO(pagedSearchDTO).GetParameters();

            List<Book> collaborators = _bookRepository.FindWithPagedSearch(query);

            if (collaborators == null || collaborators.Count == 0)
            {
                //return BaseResult<PagedSearchDTO<Book>>.Fail(BookErrors.COLLABORATOR_NOT_FOUND);
            }
            pagedSearchDTO.List = collaborators;
            pagedSearchDTO.TotalResults = _bookRepository.GetCount(queryBuilder.WithDTO(pagedSearchDTO).GetSelectCount("b", "book"), parameters);

            //return BaseResult<PagedSearchDTO<Book>>.Success(_pagedSearchBuilder.Convert(pagedSearchDTO));
            return new ObjectResult(pagedSearchDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Book book)
        {
            if (book == null) return BadRequest();
            var returnBook = _bookRepository.Add(book);
            return new ObjectResult(returnBook);
        }

        [HttpPut]
        public IActionResult Update([FromBody]Book book)
        {
            var returnBook = new Book();
            var result = _bookRepository.Exists(book.Id);
            if (!result) return this.BadRequest();
            _bookRepository.Update(book);
            return new ObjectResult(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (id == null || "".Equals(id)) return BadRequest();
            var result = _bookRepository.Exists(id);
            if (!result) return NotFound();
            _bookRepository.Remove(id);
            return new NoContentResult();
        }
    }
}