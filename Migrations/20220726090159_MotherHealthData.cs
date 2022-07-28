using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class MotherHealthData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistrictWiseMotherHealthRisk",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    ant_care1 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ant_care4 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ant_care_ub = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    neo_tetanus = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    inst_deliv = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    caesar = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    pn_health = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictWiseMotherHealthRisk", x => x.id);
                    table.ForeignKey(
                        name: "FK_DistrictWiseMotherHealthRisk_lkp_admin_boundary_districts_di~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistrictWiseMotherHealthRisk_dist_geo_code",
                table: "DistrictWiseMotherHealthRisk",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistrictWiseMotherHealthRisk");
        }
    }
}
