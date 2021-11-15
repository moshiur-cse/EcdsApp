using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class update_layer_model_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "file_lat_name",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "file_long_name",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "layer_main_attribure_name",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "admin_code",
                table: "tbl_layer_legend_color",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_lat_name",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "file_long_name",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "layer_main_attribure_name",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "admin_code",
                table: "tbl_layer_legend_color");
        }
    }
}
