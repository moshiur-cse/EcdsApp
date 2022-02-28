using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class Add_RailfallModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_future_projection_rainfall_4_point_5",
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
                    table.PrimaryKey("PK_tbl_future_projection_rainfall_4_point_5", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_future_projection_rainfall_4_point_5_lkp_admin_boundary_~",
                        column: x => x.union_geo_code,
                        principalTable: "lkp_admin_boundary_unions",
                        principalColumn: "union_geo_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_future_projection_rainfall_4_point_5_union_geo_code",
                table: "tbl_future_projection_rainfall_4_point_5",
                column: "union_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_future_projection_rainfall_4_point_5");
        }
    }
}
