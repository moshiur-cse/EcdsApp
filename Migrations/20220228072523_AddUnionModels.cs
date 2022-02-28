using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class AddUnionModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lkp_admin_boundary_unions",
                columns: table => new
                {
                    union_geo_code = table.Column<string>(type: "varchar(13)", nullable: false),
                    old_geo_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    union_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    union_name_bangla = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    upz_geo_code = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    municipality_geo_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    municipality_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    sorting_order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_admin_boundary_unions", x => x.union_geo_code);
                    table.ForeignKey(
                        name: "FK_lkp_admin_boundary_unions_lkp_admin_boundary_upazilas_upz_ge~",
                        column: x => x.upz_geo_code,
                        principalTable: "lkp_admin_boundary_upazilas",
                        principalColumn: "upz_geo_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_futute_projection_rainfall_4_point_5",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    union_geo_code = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    year_2020_2039 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2040_2059 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2060_2079 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2080_2099 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_futute_projection_rainfall_4_point_5", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_futute_projection_rainfall_4_point_5_lkp_admin_boundary_~",
                        column: x => x.union_geo_code,
                        principalTable: "lkp_admin_boundary_unions",
                        principalColumn: "union_geo_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lkp_admin_boundary_unions_upz_geo_code",
                table: "lkp_admin_boundary_unions",
                column: "upz_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_futute_projection_rainfall_4_point_5_union_geo_code",
                table: "tbl_futute_projection_rainfall_4_point_5",
                column: "union_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_futute_projection_rainfall_4_point_5");

            migrationBuilder.DropTable(
                name: "lkp_admin_boundary_unions");
        }
    }
}
