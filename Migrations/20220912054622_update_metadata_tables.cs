using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class update_metadata_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "column_id",
                table: "tbl_metadata_details",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sub_layer",
                table: "tbl_metadata_details",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_metadata_details_column_id",
                table: "tbl_metadata_details",
                column: "column_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_metadata_details_tbl_table_column_info_column_id",
                table: "tbl_metadata_details",
                column: "column_id",
                principalTable: "tbl_table_column_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_metadata_details_tbl_table_column_info_column_id",
                table: "tbl_metadata_details");

            migrationBuilder.DropIndex(
                name: "IX_tbl_metadata_details_column_id",
                table: "tbl_metadata_details");

            migrationBuilder.DropColumn(
                name: "column_id",
                table: "tbl_metadata_details");

            migrationBuilder.DropColumn(
                name: "sub_layer",
                table: "tbl_metadata_details");
        }
    }
}
