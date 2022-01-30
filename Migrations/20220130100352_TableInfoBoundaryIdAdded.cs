using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class TableInfoBoundaryIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "boundary_id",
                table: "tbl_table_info",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_table_info_boundary_id",
                table: "tbl_table_info",
                column: "boundary_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_table_info_lkp_boundary_info_boundary_id",
                table: "tbl_table_info",
                column: "boundary_id",
                principalTable: "lkp_boundary_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_table_info_lkp_boundary_info_boundary_id",
                table: "tbl_table_info");

            migrationBuilder.DropIndex(
                name: "IX_tbl_table_info_boundary_id",
                table: "tbl_table_info");

            migrationBuilder.DropColumn(
                name: "boundary_id",
                table: "tbl_table_info");
        }
    }
}
