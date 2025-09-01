using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewProper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Spots",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Spots");
        }
    }
}
