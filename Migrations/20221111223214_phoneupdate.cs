using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lamorenita.Migrations
{
    public partial class phoneupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDirection_Contact_ContactId",
                table: "ContactDirection");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_Contact_ContactId",
                table: "PhoneNumber");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "PhoneNumber",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "ContactDirection",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDirection_Contact_ContactId",
                table: "ContactDirection",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_Contact_ContactId",
                table: "PhoneNumber",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDirection_Contact_ContactId",
                table: "ContactDirection");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_Contact_ContactId",
                table: "PhoneNumber");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "PhoneNumber",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "ContactDirection",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDirection_Contact_ContactId",
                table: "ContactDirection",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_Contact_ContactId",
                table: "PhoneNumber",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id");
        }
    }
}
