using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class Update_admin_boundaries_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fill_color_code",
                table: "tbl_theme_layer_details",
                type: "varchar(7)",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "fill_opacity",
                table: "tbl_theme_layer_details",
                type: "decimal(1, 1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "line_color_code",
                table: "tbl_theme_layer_details",
                type: "varchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "line_weight",
                table: "tbl_theme_layer_details",
                type: "decimal(1, 1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "opacity",
                table: "tbl_theme_layer_details",
                type: "decimal(1, 1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "upz_geo_code",
                table: "lkp_admin_boundary_upazilas",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AddColumn<string>(
                name: "city_geo_code",
                table: "lkp_admin_boundary_upazilas",
                type: "varchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city_name",
                table: "lkp_admin_boundary_upazilas",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "old_geo_code",
                table: "lkp_admin_boundary_upazilas",
                type: "varchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "old_geo_code",
                table: "lkp_admin_boundary_divisions",
                type: "varchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "old_geo_code",
                table: "lkp_admin_boundary_districts",
                type: "varchar(4)",
                maxLength: 4,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fill_color_code",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "fill_opacity",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "line_color_code",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "line_weight",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "opacity",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "city_geo_code",
                table: "lkp_admin_boundary_upazilas");

            migrationBuilder.DropColumn(
                name: "city_name",
                table: "lkp_admin_boundary_upazilas");

            migrationBuilder.DropColumn(
                name: "old_geo_code",
                table: "lkp_admin_boundary_upazilas");

            migrationBuilder.DropColumn(
                name: "old_geo_code",
                table: "lkp_admin_boundary_divisions");

            migrationBuilder.DropColumn(
                name: "old_geo_code",
                table: "lkp_admin_boundary_districts");

            migrationBuilder.AlterColumn<string>(
                name: "upz_geo_code",
                table: "lkp_admin_boundary_upazilas",
                type: "varchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8);
        }
    }
}
