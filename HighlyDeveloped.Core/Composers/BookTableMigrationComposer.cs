using HighlyDeveloped.Core.Migrations;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace HighlyDeveloped.Core.Composers
{
    public class BookTableMigrationComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<BookTableMigrationComponent>();
        }
    }
}