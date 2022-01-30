using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class LegendColorOptionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_legend_color",
                table: "tbl_theme_layer_details");

            migrationBuilder.AddColumn<int>(
                name: "legend_color_option_id",
                table: "tbl_theme_layer_details",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "boundary_name",
                table: "lkp_boundary_info",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "attribute_name",
                table: "lkp_boundary_info",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "boundary_path",
                table: "lkp_boundary_info",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "lkp_legend_color_option",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    option_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_legend_color_option", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_theme_layer_details_legend_color_option_id",
                table: "tbl_theme_layer_details",
                column: "legend_color_option_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_theme_layer_details_lkp_legend_color_option_legend_color~",
                table: "tbl_theme_layer_details",
                column: "legend_color_option_id",
                principalTable: "lkp_legend_color_option",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_theme_layer_details_lkp_legend_color_option_legend_color~",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropTable(
                name: "lkp_legend_color_option");

            migrationBuilder.DropIndex(
                name: "IX_tbl_theme_layer_details_legend_color_option_id",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "legend_color_option_id",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "attribute_name",
                table: "lkp_boundary_info");

            migrationBuilder.DropColumn(
                name: "boundary_path",
                table: "lkp_boundary_info");

            migrationBuilder.AddColumn<bool>(
                name: "is_legend_color",
                table: "tbl_theme_layer_details",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "boundary_name",
                table: "lkp_boundary_info",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);
        }
    }
}
