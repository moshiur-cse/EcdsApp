using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class UserRoleInAppUserTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_role_id",
                table: "users",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_user_role_id",
                table: "users",
                column: "user_role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_role_lists_user_role_id",
                table: "users",
                column: "user_role_id",
                principalTable: "user_role_lists",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_user_role_lists_user_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_user_role_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "user_role_id",
                table: "users");
        }
    }
}
