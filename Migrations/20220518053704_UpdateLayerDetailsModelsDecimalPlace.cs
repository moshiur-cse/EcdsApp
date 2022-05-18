using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class UpdateLayerDetailsModelsDecimalPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "opacity",
                table: "tbl_theme_layer_details",
                type: "decimal(4, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(1, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "fill_opacity",
                table: "tbl_theme_layer_details",
                type: "decimal(4, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(1, 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "opacity",
                table: "tbl_theme_layer_details",
                type: "decimal(1, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "fill_opacity",
                table: "tbl_theme_layer_details",
                type: "decimal(1, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 2)");
        }
    }
}
