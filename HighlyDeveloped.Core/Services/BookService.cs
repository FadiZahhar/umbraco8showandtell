using HighlyDeveloped.Core.Models;
using HighlyDeveloped.Core.Repositories;
using System.Collections.Generic;

namespace HighlyDeveloped.Core.Services
{
    public class BookService
    {
        private readonly BookRepository _repo;
        public BookService(BookRepository repo) { _repo = repo; }

        public IEnumerable<Book> GetAll() => _repo.GetAll();
        public Book GetById(int id) => _repo.GetById(id);
        public void Add(Book book) => _repo.Insert(book);
        public void Update(Book book) => _repo.Update(book);
        public void Delete(int id) => _repo.Delete(id);
        public (IEnumerable<Book> Books, int TotalCount) Search(string term, int page, int pageSize)
    => _repo.Search(term, page, pageSize);
    }
}