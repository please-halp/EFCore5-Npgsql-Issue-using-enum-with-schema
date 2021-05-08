using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication;

namespace WebApplication.Migrations
{
    public partial class TestWithoutFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "schemaname");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:schemaname.my_enum", "a,b");

            migrationBuilder.CreateTable(
                name: "MyEntity",
                schema: "schemaname",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SomeProperty = table.Column<Constants.MyEnum>(type: "my_enum", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyEntity", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyEntity",
                schema: "schemaname");
        }
    }
}
