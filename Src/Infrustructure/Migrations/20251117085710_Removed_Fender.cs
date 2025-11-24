using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Fender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Reservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Reservations",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Reservations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
