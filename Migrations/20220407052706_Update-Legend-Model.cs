using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class UpdateLegendModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "icon_size",
                table: "tbl_layer_legend_colors",
                type: "decimal(4, 2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "legend_column_name",
                table: "tbl_layer_legend_colors",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "legend_column_name",
                table: "tbl_layer_legend_colors");

            migrationBuilder.AlterColumn<int>(
                name: "icon_size",
                table: "tbl_layer_legend_colors",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 2)");
        }
    }
}
