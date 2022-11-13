using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlkemyWallet.Migrations
{
    public partial class UserPasswordFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ammount",
                table: "Transactions",
                newName: "Amount");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 9, 13, 15, 2, 37, 161, DateTimeKind.Utc).AddTicks(7638));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 8, 13, 15, 2, 37, 161, DateTimeKind.Utc).AddTicks(7643));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "Ammount");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 9, 13, 14, 58, 52, 255, DateTimeKind.Utc).AddTicks(6899));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 8, 13, 14, 58, 52, 255, DateTimeKind.Utc).AddTicks(6903));
        }
    }
}
