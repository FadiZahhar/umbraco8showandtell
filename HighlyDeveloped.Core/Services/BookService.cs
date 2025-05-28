using HighlyDeveloped.Core.Models;
using HighlyDeveloped.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//service linked to book repo immediately to manipulate content through db
//logic way to implement on any controller
// used to manipulate CRUD op
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
    }
}