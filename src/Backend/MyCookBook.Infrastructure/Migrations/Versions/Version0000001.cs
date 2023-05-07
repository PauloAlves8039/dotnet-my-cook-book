using FluentMigrator;

namespace MyCookBook.Infrastructure.Migrations.Versions
{
    [Migration((long)NumberVersions.CreateUserTable, "create user table")]
    public class Version0000001 : Migration
    {
        public override void Down() { }

        public override void Up()
        {
            var table = BaseVersion.InsertDefaultColumns(Create.Table("User"));

            table
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable()
                .WithColumn("Phone").AsString(14).NotNullable();
        }
    }
}
