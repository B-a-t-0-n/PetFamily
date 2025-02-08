using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_breed_species_breed_id",
                table: "breed");

            migrationBuilder.RenameColumn(
                name: "SpeciesId",
                table: "pet",
                newName: "species_Id");

            migrationBuilder.RenameColumn(
                name: "breed_id",
                table: "breed",
                newName: "species_id");

            migrationBuilder.RenameIndex(
                name: "ix_breed_breed_id",
                table: "breed",
                newName: "ix_breed_species_id");

            migrationBuilder.AddForeignKey(
                name: "fk_breed_species_species_id",
                table: "breed",
                column: "species_id",
                principalTable: "species",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_breed_species_species_id",
                table: "breed");

            migrationBuilder.RenameColumn(
                name: "species_Id",
                table: "pet",
                newName: "SpeciesId");

            migrationBuilder.RenameColumn(
                name: "species_id",
                table: "breed",
                newName: "breed_id");

            migrationBuilder.RenameIndex(
                name: "ix_breed_species_id",
                table: "breed",
                newName: "ix_breed_breed_id");

            migrationBuilder.AddForeignKey(
                name: "fk_breed_species_breed_id",
                table: "breed",
                column: "breed_id",
                principalTable: "species",
                principalColumn: "id");
        }
    }
}
