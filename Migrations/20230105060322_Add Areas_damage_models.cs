using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class AddAreas_damage_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "area_of_lands_in_acres",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    homestead = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    garden_nursery = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    crop_land = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    pondwetland = table.Column<decimal>(name: "pond/wetland", type: "decimal(10,2)", nullable: true),
                    waste_or_otherland = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_area_of_lands_in_acres", x => x.id);
                    table.ForeignKey(
                        name: "FK_area_of_lands_in_acres_lkp_admin_boundary_districts_dist_geo~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "damage_value_of_lands_millions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    homestead = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    garden_nursery = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    crop_land = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    pondwetland = table.Column<decimal>(name: "pond/wetland", type: "decimal(10,2)", nullable: true),
                    waste_or_otherland = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_damage_value_of_lands_millions", x => x.id);
                    table.ForeignKey(
                        name: "FK_damage_value_of_lands_millions_lkp_admin_boundary_districts_~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "loss_and_damage_of_properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    dwelling_house = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    kitchen_cowshed = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    market_shop = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    tubewell_and_other = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    homestead_forestry = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loss_and_damage_of_properties", x => x.id);
                    table.ForeignKey(
                        name: "FK_loss_and_damage_of_properties_lkp_admin_boundary_districts_d~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_area_of_lands_in_acres_dist_geo_code",
                table: "area_of_lands_in_acres",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_damage_value_of_lands_millions_dist_geo_code",
                table: "damage_value_of_lands_millions",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_loss_and_damage_of_properties_dist_geo_code",
                table: "loss_and_damage_of_properties",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "area_of_lands_in_acres");

            migrationBuilder.DropTable(
                name: "damage_value_of_lands_millions");

            migrationBuilder.DropTable(
                name: "loss_and_damage_of_properties");
        }
    }
}
