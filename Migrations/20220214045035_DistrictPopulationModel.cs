using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class DistrictPopulationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_district_wise_population",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    male = table.Column<int>(type: "int", nullable: false),
                    female = table.Column<int>(type: "int", nullable: false),
                    urban = table.Column<int>(type: "int", nullable: false),
                    rural = table.Column<int>(type: "int", nullable: false),
                    other = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_district_wise_population", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_district_wise_population_lkp_admin_boundary_districts_di~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_district_wise_population_dist_geo_code",
                table: "tbl_district_wise_population",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_district_wise_population");
        }
    }
}
