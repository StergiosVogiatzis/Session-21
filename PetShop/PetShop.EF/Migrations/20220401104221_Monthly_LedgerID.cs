using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.EF.Migrations
{
    public partial class Monthly_LedgerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MonthlyLedgers",
                table: "MonthlyLedgers");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionID",
                table: "Pets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "MonthlyLedgers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonthlyLedgers",
                table: "MonthlyLedgers",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MonthlyLedgers",
                table: "MonthlyLedgers");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "MonthlyLedgers");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionID",
                table: "Pets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonthlyLedgers",
                table: "MonthlyLedgers",
                columns: new[] { "Year", "Month" });
        }
    }
}
