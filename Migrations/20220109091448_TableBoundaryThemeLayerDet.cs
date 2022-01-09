using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class TableBoundaryThemeLayerDet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "boundary_info_id",
                table: "tbl_theme_layer_details",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "table_info_id",
                table: "tbl_theme_layer_details",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_theme_layer_details_boundary_info_id",
                table: "tbl_theme_layer_details",
                column: "boundary_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_theme_layer_details_table_info_id",
                table: "tbl_theme_layer_details",
                column: "table_info_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_theme_layer_details_lkp_boundary_info_boundary_info_id",
                table: "tbl_theme_layer_details",
                column: "boundary_info_id",
                principalTable: "lkp_boundary_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_theme_layer_details_tbl_table_info_table_info_id",
                table: "tbl_theme_layer_details",
                column: "table_info_id",
                principalTable: "tbl_table_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_theme_layer_details_lkp_boundary_info_boundary_info_id",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_theme_layer_details_tbl_table_info_table_info_id",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropIndex(
                name: "IX_tbl_theme_layer_details_boundary_info_id",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropIndex(
                name: "IX_tbl_theme_layer_details_table_info_id",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "boundary_info_id",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "table_info_id",
                table: "tbl_theme_layer_details");
        }
    }
}
