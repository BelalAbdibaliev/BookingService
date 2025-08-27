using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Resources_ResourceId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ResourceId",
                table: "Bookings",
                newName: "SpotId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ResourceId",
                table: "Bookings",
                newName: "IX_Bookings_SpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Spots_SpotId",
                table: "Bookings",
                column: "SpotId",
                principalTable: "Spots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Spots_SpotId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "SpotId",
                table: "Bookings",
                newName: "ResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_SpotId",
                table: "Bookings",
                newName: "IX_Bookings_ResourceId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Resources_ResourceId",
                table: "Bookings",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
