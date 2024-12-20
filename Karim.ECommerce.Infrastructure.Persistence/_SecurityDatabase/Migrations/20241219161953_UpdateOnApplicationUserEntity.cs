using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.ECommerce.Infrastructure.Persistence._SecurityDatabase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnApplicationUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetCodeExpiry",
                table: "AspNetUsers",
                newName: "PhoneConfirmResetCodeExpiry");

            migrationBuilder.RenameColumn(
                name: "ResetCode",
                table: "AspNetUsers",
                newName: "PhoneConfirmResetCode");

            migrationBuilder.AddColumn<int>(
                name: "EmailConfirmResetCode",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailConfirmResetCodeExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmResetCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmResetCodeExpiry",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PhoneConfirmResetCodeExpiry",
                table: "AspNetUsers",
                newName: "ResetCodeExpiry");

            migrationBuilder.RenameColumn(
                name: "PhoneConfirmResetCode",
                table: "AspNetUsers",
                newName: "ResetCode");
        }
    }
}
