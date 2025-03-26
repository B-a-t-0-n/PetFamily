using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Species.Infrastructure.Migrations;

/// <inheritdoc />
public partial class SpeciesInit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "species");

        migrationBuilder.CreateTable(
            name: "species",
            schema: "species",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_species", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "breed",
            schema: "species",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                species_id = table.Column<Guid>(type: "uuid", nullable: true),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_breed", x => x.id);
                table.ForeignKey(
                    name: "fk_breed_species_species_id",
                    column: x => x.species_id,
                    principalSchema: "species",
                    principalTable: "species",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "ix_breed_species_id",
            schema: "species",
            table: "breed",
            column: "species_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "breed",
            schema: "species");

        migrationBuilder.DropTable(
            name: "species",
            schema: "species");
    }
}
