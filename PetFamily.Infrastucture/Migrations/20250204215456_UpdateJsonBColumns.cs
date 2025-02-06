using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJsonBColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailsForAssistance",
                table: "volunteer");

            migrationBuilder.DropColumn(
                name: "SocialNetwork",
                table: "volunteer");

            migrationBuilder.DropColumn(
                name: "DetailsForAssistance",
                table: "pet");

            migrationBuilder.AddColumn<string>(
                name: "details_for_assistance",
                table: "volunteer",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "social_network",
                table: "volunteer",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "details_for_assistance",
                table: "pet",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "details_for_assistance",
                table: "volunteer");

            migrationBuilder.DropColumn(
                name: "social_network",
                table: "volunteer");

            migrationBuilder.DropColumn(
                name: "details_for_assistance",
                table: "pet");

            migrationBuilder.AddColumn<string>(
                name: "DetailsForAssistance",
                table: "volunteer",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialNetwork",
                table: "volunteer",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailsForAssistance",
                table: "pet",
                type: "jsonb",
                nullable: true);
        }
    }
}
