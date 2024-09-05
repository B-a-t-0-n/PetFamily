using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class deleteNumberPets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "being_treated",
                table: "volunteer");

            migrationBuilder.DropColumn(
                name: "found_a_house",
                table: "volunteer");

            migrationBuilder.DropColumn(
                name: "looking_for_house",
                table: "volunteer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "being_treated",
                table: "volunteer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "found_a_house",
                table: "volunteer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "looking_for_house",
                table: "volunteer",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
