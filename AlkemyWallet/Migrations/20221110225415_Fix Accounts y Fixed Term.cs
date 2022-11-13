using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlkemyWallet.Migrations
{
    public partial class FixAccountsyFixedTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedTermDeposits_Accounts_AccountsEntityId",
                table: "FixedTermDeposits");

            migrationBuilder.DropIndex(
                name: "IX_FixedTermDeposits_AccountsEntityId",
                table: "FixedTermDeposits");

            migrationBuilder.DropColumn(
                name: "AccountsEntityId",
                table: "FixedTermDeposits");

            migrationBuilder.InsertData(
                table: "FixedTermDeposits",
                columns: new[] { "Id", "AccountId", "Amount", "ClosingDate", "CreationDate", "IsDeleted", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1200m, new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 5, 15, 8, 55, 0, DateTimeKind.Unspecified), false, 1 },
                    { 2, 2, 2000m, new DateTime(2022, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 1, 14, 9, 10, 55, 0, DateTimeKind.Unspecified), false, 2 },
                    { 3, 2, 2100m, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 15, 16, 9, 25, 0, DateTimeKind.Unspecified), false, 3 },
                    { 4, 4, 1400m, new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 20, 12, 35, 15, 0, DateTimeKind.Unspecified), false, 4 }
                });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 9, 10, 22, 54, 14, 515, DateTimeKind.Utc).AddTicks(3868));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 8, 10, 22, 54, 14, 515, DateTimeKind.Utc).AddTicks(3876));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FixedTermDeposits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FixedTermDeposits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FixedTermDeposits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FixedTermDeposits",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "AccountsEntityId",
                table: "FixedTermDeposits",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 9, 9, 18, 9, 56, 261, DateTimeKind.Utc).AddTicks(7072));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 8, 9, 18, 9, 56, 261, DateTimeKind.Utc).AddTicks(7078));

            migrationBuilder.CreateIndex(
                name: "IX_FixedTermDeposits_AccountsEntityId",
                table: "FixedTermDeposits",
                column: "AccountsEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedTermDeposits_Accounts_AccountsEntityId",
                table: "FixedTermDeposits",
                column: "AccountsEntityId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
