using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Accounts.Infrastructure.Migrations;

/// <inheritdoc />
public partial class UpdateUsers : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "ix_volunteer_accounts_user_id",
            schema: "accounts",
            table: "volunteer_accounts");

        migrationBuilder.DropIndex(
            name: "ix_partisipant_accounts_user_id",
            schema: "accounts",
            table: "partisipant_accounts");

        migrationBuilder.DropIndex(
            name: "ix_admin_accounts_user_id",
            schema: "accounts",
            table: "admin_accounts");

        migrationBuilder.CreateIndex(
            name: "ix_volunteer_accounts_user_id",
            schema: "accounts",
            table: "volunteer_accounts",
            column: "user_id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_partisipant_accounts_user_id",
            schema: "accounts",
            table: "partisipant_accounts",
            column: "user_id",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_admin_accounts_user_id",
            schema: "accounts",
            table: "admin_accounts",
            column: "user_id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "ix_volunteer_accounts_user_id",
            schema: "accounts",
            table: "volunteer_accounts");

        migrationBuilder.DropIndex(
            name: "ix_partisipant_accounts_user_id",
            schema: "accounts",
            table: "partisipant_accounts");

        migrationBuilder.DropIndex(
            name: "ix_admin_accounts_user_id",
            schema: "accounts",
            table: "admin_accounts");

        migrationBuilder.CreateIndex(
            name: "ix_volunteer_accounts_user_id",
            schema: "accounts",
            table: "volunteer_accounts",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_partisipant_accounts_user_id",
            schema: "accounts",
            table: "partisipant_accounts",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_admin_accounts_user_id",
            schema: "accounts",
            table: "admin_accounts",
            column: "user_id");
    }
}
