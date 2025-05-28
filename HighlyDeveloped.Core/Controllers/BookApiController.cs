using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.WebApi;
using HighlyDeveloped.Core.Models;
using HighlyDeveloped.Core.Services;

namespace HighlyDeveloped.Core.Controllers
{
    public class BookApiController : UmbracoAuthorizedApiController
    {
        private readonly BookService _service;
        public BookApiController(BookService service) { _service = service; }
        [HttpGet]
        public IEnumerable<Book> GetAll() => _service.GetAll();

        [HttpGet]
        public Book GetById(int id) => _service.GetById(id);

        [HttpPost]
        public IHttpActionResult PostSave([FromBody] Book book)
        {
            // 1. ModelState is automatically populated based on DataAnnotations
            if (!ModelState.IsValid)
            {
                // Return all validation errors to the caller
                return BadRequest(ModelState);
            }

            if (book.Id == 0)
                _service.Add(book);
            else
                _service.Update(book);

            return Ok(book);
        }

        [HttpPost]
        public IHttpActionResult PostDelete([FromBody] int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpGet]
        public object Search(string term = "", int page = 1, int pageSize = 10)
        {
            var result = _service.Search(term, page, pageSize);
            return new
            {
                books = result.Books,
                totalCount = result.TotalCount
            };
        }
    }

}