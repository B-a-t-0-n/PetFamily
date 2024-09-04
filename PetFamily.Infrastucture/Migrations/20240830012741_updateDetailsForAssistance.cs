using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class updateDetailsForAssistance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description_details_for_assistance",
                table: "volunteer");

            migrationBuilder.DropColumn(
                name: "name_details_for_assistance",
                table: "volunteer");

            migrationBuilder.AddColumn<string>(
                name: "DetailsForAssistance",
                table: "volunteer",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailsForAssistance",
                table: "volunteer");

            migrationBuilder.AddColumn<string>(
                name: "description_details_for_assistance",
                table: "volunteer",
                type: "character varying(6000)",
                maxLength: 6000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name_details_for_assistance",
                table: "volunteer",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
