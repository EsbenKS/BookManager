using Microsoft.EntityFrameworkCore.Migrations;

namespace BookManager.Data.Migrations
{
    public partial class Uniqueindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authorships_AuthorID",
                table: "Authorships");

            migrationBuilder.CreateIndex(
                name: "IX_Authorships_AuthorID_BookID",
                table: "Authorships",
                columns: new[] { "AuthorID", "BookID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authorships_AuthorID_BookID",
                table: "Authorships");

            migrationBuilder.CreateIndex(
                name: "IX_Authorships_AuthorID",
                table: "Authorships",
                column: "AuthorID");
        }
    }
}
