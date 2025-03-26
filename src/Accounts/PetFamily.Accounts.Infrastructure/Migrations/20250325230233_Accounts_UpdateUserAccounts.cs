using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Accounts_UpdateUserAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_admin_accounts_users_user_id",
                schema: "accounts",
                table: "admin_accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_partisipant_accounts_users_user_id",
                schema: "accounts",
                table: "partisipant_accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_volunteer_accounts_users_user_id",
                schema: "accounts",
                table: "volunteer_accounts");

            migrationBuilder.AddForeignKey(
                name: "fk_admin_accounts_users_user_id",
                schema: "accounts",
                table: "admin_accounts",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_partisipant_accounts_users_user_id",
                schema: "accounts",
                table: "partisipant_accounts",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_volunteer_accounts_users_user_id",
                schema: "accounts",
                table: "volunteer_accounts",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_admin_accounts_users_user_id",
                schema: "accounts",
                table: "admin_accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_partisipant_accounts_users_user_id",
                schema: "accounts",
                table: "partisipant_accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_volunteer_accounts_users_user_id",
                schema: "accounts",
                table: "volunteer_accounts");

            migrationBuilder.AddForeignKey(
                name: "fk_admin_accounts_users_user_id",
                schema: "accounts",
                table: "admin_accounts",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_partisipant_accounts_users_user_id",
                schema: "accounts",
                table: "partisipant_accounts",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_volunteer_accounts_users_user_id",
                schema: "accounts",
                table: "volunteer_accounts",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
