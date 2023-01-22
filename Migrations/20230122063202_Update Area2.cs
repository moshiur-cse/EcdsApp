using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class UpdateArea2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "disaster_affected_household_from_2015_to_2020",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    drought = table.Column<int>(type: "int", nullable: false),
                    flood = table.Column<int>(type: "int", nullable: false),
                    water_logging = table.Column<int>(type: "int", nullable: false),
                    cyclone = table.Column<int>(type: "int", nullable: false),
                    tornado = table.Column<int>(type: "int", nullable: false),
                    strom_or_tridal_surge = table.Column<int>(type: "int", nullable: false),
                    thunderstrom_or_lightning = table.Column<int>(type: "int", nullable: false),
                    river_or_coastal_erosion = table.Column<int>(type: "int", nullable: false),
                    landslide = table.Column<int>(type: "int", nullable: false),
                    salinity = table.Column<int>(type: "int", nullable: false),
                    hailstrom = table.Column<int>(type: "int", nullable: false),
                    other_disasters = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disaster_affected_household_from_2015_to_2020", x => x.id);
                    table.ForeignKey(
                        name: "FK_disaster_affected_household_from_2015_to_2020_lkp_admin_boun~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_disaster_affected_household_from_2015_to_2020_dist_geo_code",
                table: "disaster_affected_household_from_2015_to_2020",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "disaster_affected_household_from_2015_to_2020");
        }
    }
}
