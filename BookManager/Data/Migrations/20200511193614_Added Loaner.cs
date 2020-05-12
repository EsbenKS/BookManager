using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookManager.Data.Migrations
{
    public partial class AddedLoaner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Authors_AuthorId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_AuthorId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Loans");

            migrationBuilder.AddColumn<Guid>(
                name: "LoanerID",
                table: "Loans",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Loaners",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loaners", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanerID",
                table: "Loans",
                column: "LoanerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Loaners_LoanerID",
                table: "Loans",
                column: "LoanerID",
                principalTable: "Loaners",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Loaners_LoanerID",
                table: "Loans");

            migrationBuilder.DropTable(
                name: "Loaners");

            migrationBuilder.DropIndex(
                name: "IX_Loans_LoanerID",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LoanerID",
                table: "Loans");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Loans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AuthorId",
                table: "Loans",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Authors_AuthorId",
                table: "Loans",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
