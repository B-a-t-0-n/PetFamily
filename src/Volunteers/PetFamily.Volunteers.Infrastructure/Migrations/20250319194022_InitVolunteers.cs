using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Volunteers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitVolunteers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "volunteers");

            migrationBuilder.CreateTable(
                name: "volunteer",
                schema: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(6000)", maxLength: 6000, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pet",
                schema: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_castrated = table.Column<bool>(type: "boolean", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                    date_of_creation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    requisites = table.Column<string>(type: "jsonb", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    apartment_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    flat = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    house = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    assistance_status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying(6000)", maxLength: 6000, nullable: true),
                    health_information = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: true),
                    nickname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    serial_number = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<double>(type: "double precision", nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet", x => x.id);
                    table.ForeignKey(
                        name: "fk_pet_volunteer_volunteer_id",
                        column: x => x.volunteer_id,
                        principalSchema: "volunteers",
                        principalTable: "volunteer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pet_photo",
                schema: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", maxLength: 100, nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: true),
                    path_to_storage = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet_photo", x => x.id);
                    table.ForeignKey(
                        name: "fk_pet_photo_pet_pet_id",
                        column: x => x.pet_id,
                        principalSchema: "volunteers",
                        principalTable: "pet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pet_volunteer_id",
                schema: "volunteers",
                table: "pet",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_pet_photo_pet_id",
                schema: "volunteers",
                table: "pet_photo",
                column: "pet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pet_photo",
                schema: "volunteers");

            migrationBuilder.DropTable(
                name: "pet",
                schema: "volunteers");

            migrationBuilder.DropTable(
                name: "volunteer",
                schema: "volunteers");
        }
    }
}
