using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class changeduserlogtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_email",
                table: "UserLogs",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_user_email",
                table: "UserLogs",
                column: "user_email");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_users_user_email",
                table: "UserLogs",
                column: "user_email",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_users_user_email",
                table: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserLogs_user_email",
                table: "UserLogs");

            migrationBuilder.DropColumn(
                name: "user_email",
                table: "UserLogs");
        }
    }
}
