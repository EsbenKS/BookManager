using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookManager.Data.Migrations
{
    public partial class ReturnedLoanDateadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LoanReturnedDate",
                table: "Loans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanReturnedDate",
                table: "Loans");
        }
    }
}
