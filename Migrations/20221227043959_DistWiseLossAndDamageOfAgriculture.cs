using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class DistWiseLossAndDamageOfAgriculture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dist_wise_loss_and_damage_of_agriculture",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    paddy = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    potato = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    wheat = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    jute = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    pulse = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    fruits = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    other_crop = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    livestock = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    poultry_birds = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    fisheries = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    others = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dist_wise_loss_and_damage_of_agriculture", x => x.id);
                    table.ForeignKey(
                        name: "FK_dist_wise_loss_and_damage_of_agriculture_lkp_admin_boundary_~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dist_wise_loss_and_damage_of_agriculture_dist_geo_code",
                table: "dist_wise_loss_and_damage_of_agriculture",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dist_wise_loss_and_damage_of_agriculture");
        }
    }
}
