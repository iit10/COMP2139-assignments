using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartInventory3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_Users_UserId",
                schema: "Identity",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_Users_UserId",
                schema: "Identity",
                table: "UserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_USerRole_Users_UserId",
                schema: "Identity",
                table: "USerRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_Users_UserId",
                schema: "Identity",
                table: "UserToken");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "Identity",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_AspNetUsers_UserId",
                schema: "Identity",
                table: "UserClaim",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_AspNetUsers_UserId",
                schema: "Identity",
                table: "UserLogin",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USerRole_AspNetUsers_UserId",
                schema: "Identity",
                table: "USerRole",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_Id",
                schema: "Identity",
                table: "Users",
                column: "Id",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_AspNetUsers_UserId",
                schema: "Identity",
                table: "UserToken",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_AspNetUsers_UserId",
                schema: "Identity",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_AspNetUsers_UserId",
                schema: "Identity",
                table: "UserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_USerRole_AspNetUsers_UserId",
                schema: "Identity",
                table: "USerRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_Id",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_AspNetUsers_UserId",
                schema: "Identity",
                table: "UserToken");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Identity");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                schema: "Identity",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "Identity",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Identity",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                schema: "Identity",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                schema: "Identity",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                schema: "Identity",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                schema: "Identity",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                schema: "Identity",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                schema: "Identity",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Identity",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                schema: "Identity",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                schema: "Identity",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                schema: "Identity",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "Identity",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_Users_UserId",
                schema: "Identity",
                table: "UserClaim",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_Users_UserId",
                schema: "Identity",
                table: "UserLogin",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USerRole_Users_UserId",
                schema: "Identity",
                table: "USerRole",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_Users_UserId",
                schema: "Identity",
                table: "UserToken",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
