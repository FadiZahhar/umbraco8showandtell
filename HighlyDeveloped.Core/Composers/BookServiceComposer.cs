using HighlyDeveloped.Core.Repositories;
using HighlyDeveloped.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;

public class BookServiceComposer : IUserComposer
{
    public void Compose(Composition composition)
    {
        composition.Register<BookService>(Lifetime.Singleton);
        // If BookRepository is a dependency, register it too!
        composition.Register<BookRepository>(Lifetime.Singleton);
    }
}