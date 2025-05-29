using Umbraco.Core.Migrations;

namespace HighlyDeveloped.Core.Migrations
{
    public class CreateBookTableMigration : MigrationBase
    {
        public CreateBookTableMigration(IMigrationContext context) : base(context) { }

        public override void Migrate()
        {
            if (!TableExists("Books"))
            {
                Create.Table("Books")
                    .WithColumn("Id").AsInt32().PrimaryKey("PK_Books_Id").Identity()
                    .WithColumn("Title").AsString(255).NotNullable()
                    .WithColumn("Author").AsString(255).NotNullable()
                    .WithColumn("Year").AsInt32().NotNullable()
                    .WithColumn("ISBN").AsString(50).Nullable()
                    .Do();
            }
        }
    }
}