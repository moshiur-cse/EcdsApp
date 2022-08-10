using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class literacyRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_upazila_wise_literacy_rate",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    upz_geo_code = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    literacy_rate_both = table.Column<int>(type: "int", nullable: true),
                    literacy_rate_male = table.Column<int>(type: "int", nullable: true),
                    literacy_rate_female = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_upazila_wise_literacy_rate", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_upazila_wise_literacy_rate_lkp_admin_boundary_upazilas_u~",
                        column: x => x.upz_geo_code,
                        principalTable: "lkp_admin_boundary_upazilas",
                        principalColumn: "upz_geo_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_upazila_wise_literacy_rate_upz_geo_code",
                table: "tbl_upazila_wise_literacy_rate",
                column: "upz_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_upazila_wise_literacy_rate");
        }
    }
}
