using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class UpdateThemeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "theme_color",
                table: "lkp_themes",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "theme_description",
                table: "lkp_themes",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "theme_icon",
                table: "lkp_themes",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "theme_short_name",
                table: "lkp_themes",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "theme_color",
                table: "lkp_themes");

            migrationBuilder.DropColumn(
                name: "theme_description",
                table: "lkp_themes");

            migrationBuilder.DropColumn(
                name: "theme_icon",
                table: "lkp_themes");

            migrationBuilder.DropColumn(
                name: "theme_short_name",
                table: "lkp_themes");
        }
    }
}
