using Umbraco.Core.Composing;
using Umbraco.Core.Migrations;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;
using Umbraco.Core.Logging;
using Umbraco.Core.Migrations.Upgrade;

namespace HighlyDeveloped.Core.Migrations
{
    public class BookTableMigrationComponent : IComponent
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly IMigrationBuilder _migrationBuilder;
        private readonly IKeyValueService _keyValueService;
        private readonly ILogger _logger;

        public BookTableMigrationComponent(
            IScopeProvider scopeProvider,
            IMigrationBuilder migrationBuilder,
            IKeyValueService keyValueService,
            ILogger logger)
        {
            _scopeProvider = scopeProvider;
            _migrationBuilder = migrationBuilder;
            _keyValueService = keyValueService;
            _logger = logger;
        }

        public void Initialize()
        {
            var migrationPlan = new MigrationPlan("BookTable");
            migrationPlan.From(string.Empty)
                .To<CreateBookTableMigration>("create-book-table");
            var upgrader = new Upgrader(migrationPlan);

            upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
        }

        public void Terminate() { }
    }
}