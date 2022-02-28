using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class RoleWiseComponentMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "permitted_to_add_edit",
                table: "user_role_wise_components",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permitted_to_delete",
                table: "user_role_wise_components",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permitted_to_download_data",
                table: "user_role_wise_components",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permitted_to_view",
                table: "user_role_wise_components",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "permitted_to_add_edit",
                table: "user_role_wise_components");

            migrationBuilder.DropColumn(
                name: "permitted_to_delete",
                table: "user_role_wise_components");

            migrationBuilder.DropColumn(
                name: "permitted_to_download_data",
                table: "user_role_wise_components");

            migrationBuilder.DropColumn(
                name: "permitted_to_view",
                table: "user_role_wise_components");
        }
    }
}
