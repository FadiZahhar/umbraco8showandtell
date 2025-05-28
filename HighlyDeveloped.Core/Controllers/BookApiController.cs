using HighlyDeveloped.Core.Models;
using HighlyDeveloped.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Web.WebApi;

//fetch from service and display json

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
        public void PostSave([FromBody] Book book)
        {
            if (book.Id == 0)
                _service.Add(book);
            else
                _service.Update(book);
        }

        [HttpPost]
        public void PostDelete([FromBody] int id) => _service.Delete(id);
    }
}