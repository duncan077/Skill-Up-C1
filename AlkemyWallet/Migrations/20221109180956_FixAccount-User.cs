using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlkemyWallet.Migrations
{
    public partial class FixAccountUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "Accounts",
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
                name: "IX_Accounts_UserEntityId",
                table: "Accounts",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserEntityId",
                table: "Accounts",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserEntityId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserEntityId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 9, 6, 17, 36, 38, 88, DateTimeKind.Utc).AddTicks(9649));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 8, 6, 17, 36, 38, 88, DateTimeKind.Utc).AddTicks(9653));

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId",
                unique: true);
        }
    }
}
