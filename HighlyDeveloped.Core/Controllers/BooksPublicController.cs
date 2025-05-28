using HighlyDeveloped.Core.Models;
using HighlyDeveloped.Core.Services;
using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.WebApi;

public class BooksPublicController : UmbracoApiController
{
    private readonly BookService _service;
    public BooksPublicController(BookService service) { _service = service; }

    [HttpGet]
    public IEnumerable<Book> GetAllBooks() => _service.GetAll();
}