using HighlyDeveloped.Core.Models;
using NPoco;
using System.Collections.Generic;
using Umbraco.Core.Scoping;

namespace HighlyDeveloped.Core.Repositories
{
    public class BookRepository
    {
        private readonly IScopeProvider _scopeProvider;

        public BookRepository(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public IEnumerable<Book> GetAll()
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                var books = scope.Database.Fetch<Book>("SELECT * FROM Books");
                return books;
            }
        }

        public Book GetById(int id)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return scope.Database.SingleOrDefault<Book>("SELECT * FROM Books WHERE Id = @0", id);
            }
        }

        public void Insert(Book book)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Insert(book);
                scope.Complete();
            }
        }

        public void Update(Book book)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Update(book);
                scope.Complete();
            }
        }

        public void Delete(int id)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Execute("DELETE FROM Books WHERE Id = @0", id);
                scope.Complete();
            }
        }
    }
}