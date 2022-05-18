using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class UpdateLayerDetailsModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_attribute_code",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "first_attribute_display_name",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "first_attribute_name",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "second_attribute_code",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "second_attribute_display_name",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "second_attribute_name",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "third_attribute_code",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "third_attribute_display_name",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "third_attribute_name",
                table: "tbl_theme_layer_details");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_attribute_code",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "first_attribute_display_name",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "first_attribute_name",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "second_attribute_code",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "second_attribute_display_name",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "second_attribute_name",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "third_attribute_code",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "third_attribute_display_name",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "third_attribute_name",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
