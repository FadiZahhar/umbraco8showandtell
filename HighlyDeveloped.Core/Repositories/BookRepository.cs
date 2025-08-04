using HighlyDeveloped.Core.Models;
using NPoco;
using System.Collections.Generic;
using System.Linq;
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
                return scope.Database.Fetch<Book>("SELECT * FROM Books");
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

        public (IEnumerable<Book> Books, int TotalCount) Search(string term, int page, int pageSize)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                var hasTerm = !string.IsNullOrWhiteSpace(term);
                var where = hasTerm
                    ? "WHERE Title LIKE @0 OR Author LIKE @0 OR ISBN LIKE @0"
                    : "";

                var query = $"SELECT * FROM Books {where} ORDER BY Id DESC OFFSET @{(hasTerm ? 1 : 0)} ROWS FETCH NEXT @{(hasTerm ? 2 : 1)} ROWS ONLY";
                var countQuery = $"SELECT COUNT(*) FROM Books {where}";

                var parameters = hasTerm
                    ? new object[] { $"%{term}%", (page - 1) * pageSize, pageSize }
                    : new object[] { (page - 1) * pageSize, pageSize };

                var books = scope.Database.Fetch<Book>(query, parameters);
                var count = scope.Database.ExecuteScalar<int>(countQuery, hasTerm ? new object[] { $"%{term}%" } : null);

                return (books, count);
            }
        }

    }
}