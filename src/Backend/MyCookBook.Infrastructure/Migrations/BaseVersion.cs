using FluentMigrator.Builders.Create.Table;

namespace MyCookBook.Infrastructure.Migrations
{
    public static class BaseVersion
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax InsertDefaultColumns(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table) 
        {
            return table
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CreationDate").AsString().NotNullable();
        }
    }
}
