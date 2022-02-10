using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class AddDistrictWisePoverty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_district_wise_poverty",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    poverty_rate = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_district_wise_poverty", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_district_wise_poverty_lkp_admin_boundary_districts_dist_~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_district_wise_poverty_dist_geo_code",
                table: "tbl_district_wise_poverty",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_district_wise_poverty");
        }
    }
}
