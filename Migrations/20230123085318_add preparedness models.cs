using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class addpreparednessmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "disaster_aff_hh_cat_preparednes_2015_to_2020_div",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    div_geo_code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    brick_built = table.Column<int>(type: "int", nullable: false),
                    semi_brick_built = table.Column<int>(type: "int", nullable: false),
                    strengthen_infrastructure = table.Column<int>(type: "int", nullable: false),
                    tube_well_for_drinking_water = table.Column<int>(type: "int", nullable: false),
                    improved_sanitation = table.Column<int>(type: "int", nullable: false),
                    raise_road_for_communication = table.Column<int>(type: "int", nullable: false),
                    send_school_going_children_to_safe_place = table.Column<int>(type: "int", nullable: false),
                    increased_security_for_family_food_storage = table.Column<int>(type: "int", nullable: false),
                    raised_house = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disaster_aff_hh_cat_preparednes_2015_to_2020_div", x => x.id);
                    table.ForeignKey(
                        name: "FK_disaster_aff_hh_cat_preparednes_2015_to_2020_div_lkp_admin_b~",
                        column: x => x.div_geo_code,
                        principalTable: "lkp_admin_boundary_divisions",
                        principalColumn: "div_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "disaster_aff_n_preparednes_hh_2015_to_2020_dist",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    household_preparedness = table.Column<int>(type: "int", nullable: false),
                    household_not_preparedness = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disaster_aff_n_preparednes_hh_2015_to_2020_dist", x => x.id);
                    table.ForeignKey(
                        name: "FK_disaster_aff_n_preparednes_hh_2015_to_2020_dist_lkp_admin_bo~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_disaster_aff_hh_cat_preparednes_2015_to_2020_div_div_geo_code",
                table: "disaster_aff_hh_cat_preparednes_2015_to_2020_div",
                column: "div_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_disaster_aff_n_preparednes_hh_2015_to_2020_dist_dist_geo_code",
                table: "disaster_aff_n_preparednes_hh_2015_to_2020_dist",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "disaster_aff_hh_cat_preparednes_2015_to_2020_div");

            migrationBuilder.DropTable(
                name: "disaster_aff_n_preparednes_hh_2015_to_2020_dist");
        }
    }
}
