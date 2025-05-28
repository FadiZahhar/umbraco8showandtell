using HighlyDeveloped.Core.Models;
using HighlyDeveloped.Core.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Umbraco.Web.WebApi;

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


        [HttpGet]
        public HttpResponseMessage ExportCsv()
        {
            var books = _service.GetAll();
            var sb = new StringBuilder();
            sb.AppendLine("Id,Title,Author,Year,ISBN");
            foreach (var book in books)
                sb.AppendLine($"{book.Id},\"{book.Title}\",\"{book.Author}\",{book.Year},\"{book.ISBN}\"");

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(sb.ToString(), Encoding.UTF8, "text/csv")
            };
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = "books-export.csv"
            };
            return result;
        }

    }

}