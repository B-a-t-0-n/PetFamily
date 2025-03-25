using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Accounts_UpdateAccountsUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_admin_accounts_users_user_id1",
                schema: "accounts",
                table: "admin_accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_partisipant_accounts_users_user_id1",
                schema: "accounts",
                table: "partisipant_accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_volunteer_accounts_users_user_id1",
                schema: "accounts",
                table: "volunteer_accounts");

            migrationBuilder.DropIndex(
                name: "ix_volunteer_accounts_user_id1",
                schema: "accounts",
                table: "volunteer_accounts");

            migrationBuilder.DropIndex(
                name: "ix_partisipant_accounts_user_id1",
                schema: "accounts",
                table: "partisipant_accounts");

            migrationBuilder.DropIndex(
                name: "ix_admin_accounts_user_id1",
                schema: "accounts",
                table: "admin_accounts");

            migrationBuilder.DropColumn(
                name: "user_id1",
                schema: "accounts",
                table: "volunteer_accounts");

            migrationBuilder.DropColumn(
                name: "user_id1",
                schema: "accounts",
                table: "partisipant_accounts");

            migrationBuilder.DropColumn(
                name: "user_id1",
                schema: "accounts",
                table: "admin_accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id1",
                schema: "accounts",
                table: "volunteer_accounts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "user_id1",
                schema: "accounts",
                table: "partisipant_accounts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "user_id1",
                schema: "accounts",
                table: "admin_accounts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_volunteer_accounts_user_id1",
                schema: "accounts",
                table: "volunteer_accounts",
                column: "user_id1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_partisipant_accounts_user_id1",
                schema: "accounts",
                table: "partisipant_accounts",
                column: "user_id1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_admin_accounts_user_id1",
                schema: "accounts",
                table: "admin_accounts",
                column: "user_id1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_admin_accounts_users_user_id1",
                schema: "accounts",
                table: "admin_accounts",
                column: "user_id1",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_partisipant_accounts_users_user_id1",
                schema: "accounts",
                table: "partisipant_accounts",
                column: "user_id1",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_volunteer_accounts_users_user_id1",
                schema: "accounts",
                table: "volunteer_accounts",
                column: "user_id1",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
