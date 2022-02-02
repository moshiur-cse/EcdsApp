using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class LineWeightModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "line_weight",
                table: "tbl_theme_layer_details",
                type: "decimal(4, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2, 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "line_weight",
                table: "tbl_theme_layer_details",
                type: "decimal(2, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 2)");
        }
    }
}
