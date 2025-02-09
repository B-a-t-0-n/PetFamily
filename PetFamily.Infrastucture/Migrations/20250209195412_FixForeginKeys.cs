using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class FixForeginKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pet_volunteer_volunteer_id",
                table: "pet");

            migrationBuilder.DropForeignKey(
                name: "fk_pet_photo_pet_pet_id",
                table: "pet_photo");

            migrationBuilder.AddForeignKey(
                name: "fk_pet_volunteer_volunteer_id",
                table: "pet",
                column: "volunteer_id",
                principalTable: "volunteer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_pet_photo_pet_pet_id",
                table: "pet_photo",
                column: "pet_id",
                principalTable: "pet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pet_volunteer_volunteer_id",
                table: "pet");

            migrationBuilder.DropForeignKey(
                name: "fk_pet_photo_pet_pet_id",
                table: "pet_photo");

            migrationBuilder.AddForeignKey(
                name: "fk_pet_volunteer_volunteer_id",
                table: "pet",
                column: "volunteer_id",
                principalTable: "volunteer",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_pet_photo_pet_pet_id",
                table: "pet_photo",
                column: "pet_id",
                principalTable: "pet",
                principalColumn: "id");
        }
    }
}
