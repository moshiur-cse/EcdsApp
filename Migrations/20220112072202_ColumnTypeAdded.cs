using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class ColumnTypeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "column_type_id",
                table: "tbl_table_column_info",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "lkp_column_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_column_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_upazila_wise_risk_index",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    upz_geo_code = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    people_affected_natural_disaster = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    heat_stress_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ground_water_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    mangrove_forest_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    live_stock_land_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    water_availability_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    crop_yield_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    livestock_health_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    agri_land_availability_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    fish_culture_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    fish_capture_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    rail_network_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    road_network_vulnerability = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_upazila_wise_risk_index", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_upazila_wise_risk_index_lkp_admin_boundary_upazilas_upz_~",
                        column: x => x.upz_geo_code,
                        principalTable: "lkp_admin_boundary_upazilas",
                        principalColumn: "upz_geo_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_table_column_info_column_type_id",
                table: "tbl_table_column_info",
                column: "column_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_upazila_wise_risk_index_upz_geo_code",
                table: "tbl_upazila_wise_risk_index",
                column: "upz_geo_code");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_table_column_info_lkp_column_type_column_type_id",
                table: "tbl_table_column_info",
                column: "column_type_id",
                principalTable: "lkp_column_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_table_column_info_lkp_column_type_column_type_id",
                table: "tbl_table_column_info");

            migrationBuilder.DropTable(
                name: "lkp_column_type");

            migrationBuilder.DropTable(
                name: "tbl_upazila_wise_risk_index");

            migrationBuilder.DropIndex(
                name: "IX_tbl_table_column_info_column_type_id",
                table: "tbl_table_column_info");

            migrationBuilder.DropColumn(
                name: "column_type_id",
                table: "tbl_table_column_info");
        }
    }
}
