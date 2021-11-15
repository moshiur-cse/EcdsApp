using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class update_layer_model_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "admin_code",
                table: "tbl_layer_legend_color");

            migrationBuilder.AddColumn<string>(
                name: "layer_main_attribure_code",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "layer_main_attribure_one",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "layer_main_attribure_three",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "layer_main_attribure_two",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "layer_main_attribure_value",
                table: "tbl_layer_legend_color",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "layer_main_attribure_code",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "layer_main_attribure_one",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "layer_main_attribure_three",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "layer_main_attribure_two",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "layer_main_attribure_value",
                table: "tbl_layer_legend_color");

            migrationBuilder.AddColumn<string>(
                name: "admin_code",
                table: "tbl_layer_legend_color",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
