﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.ECommerce.Infrastructure.Persistence._SecurityDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddedResetCodeToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResetCode",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetCodeExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetCodeExpiry",
                table: "AspNetUsers");
        }
    }
}
