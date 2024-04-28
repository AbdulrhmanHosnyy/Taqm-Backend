using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taqm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUserRefreshTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRefreshTokens_AspNetUsers_UserId",
                table: "UserRefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRefreshTokens",
                table: "UserRefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_UserRefreshTokens_UserId",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "JwtId",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UserRefreshTokens");

            migrationBuilder.RenameTable(
                name: "UserRefreshTokens",
                newName: "UserRefreshToken");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "UserRefreshToken",
                newName: "ExpiresOn");

            migrationBuilder.RenameColumn(
                name: "AddedTime",
                table: "UserRefreshToken",
                newName: "CreatedOn");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "UserRefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedOn",
                table: "UserRefreshToken",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRefreshToken",
                table: "UserRefreshToken",
                columns: new[] { "UserId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRefreshToken_AspNetUsers_UserId",
                table: "UserRefreshToken",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRefreshToken_AspNetUsers_UserId",
                table: "UserRefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRefreshToken",
                table: "UserRefreshToken");

            migrationBuilder.DropColumn(
                name: "RevokedOn",
                table: "UserRefreshToken");

            migrationBuilder.RenameTable(
                name: "UserRefreshToken",
                newName: "UserRefreshTokens");

            migrationBuilder.RenameColumn(
                name: "ExpiresOn",
                table: "UserRefreshTokens",
                newName: "ExpiryDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "UserRefreshTokens",
                newName: "AddedTime");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "UserRefreshTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "UserRefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "UserRefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JwtId",
                table: "UserRefreshTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UserRefreshTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRefreshTokens",
                table: "UserRefreshTokens",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                table: "UserRefreshTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRefreshTokens_AspNetUsers_UserId",
                table: "UserRefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
