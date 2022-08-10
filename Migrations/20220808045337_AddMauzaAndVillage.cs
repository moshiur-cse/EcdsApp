using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class AddMauzaAndVillage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lkp_admin_boundary_mauza",
                columns: table => new
                {
                    mauza_geo_code = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    old_geo_code = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    mauza_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    mauza_name_bangla = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    union_geo_code = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_admin_boundary_mauza", x => x.mauza_geo_code);
                    table.ForeignKey(
                        name: "FK_lkp_admin_boundary_mauza_lkp_admin_boundary_unions_union_geo~",
                        column: x => x.union_geo_code,
                        principalTable: "lkp_admin_boundary_unions",
                        principalColumn: "union_geo_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lkp_admin_boundary_village",
                columns: table => new
                {
                    village_geo_code = table.Column<string>(type: "varchar(19)", maxLength: 19, nullable: false),
                    old_geo_code = table.Column<string>(type: "varchar(19)", maxLength: 19, nullable: true),
                    village_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    village_name_bangla = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    mauza_geo_code = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_admin_boundary_village", x => x.village_geo_code);
                    table.ForeignKey(
                        name: "FK_lkp_admin_boundary_village_lkp_admin_boundary_mauza_mauza_ge~",
                        column: x => x.mauza_geo_code,
                        principalTable: "lkp_admin_boundary_mauza",
                        principalColumn: "mauza_geo_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lkp_admin_boundary_mauza_union_geo_code",
                table: "lkp_admin_boundary_mauza",
                column: "union_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_lkp_admin_boundary_village_mauza_geo_code",
                table: "lkp_admin_boundary_village",
                column: "mauza_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lkp_admin_boundary_village");

            migrationBuilder.DropTable(
                name: "lkp_admin_boundary_mauza");
        }
    }
}
