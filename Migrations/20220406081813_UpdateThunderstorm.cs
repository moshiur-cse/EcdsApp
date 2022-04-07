using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class UpdateThunderstorm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "thunderstm",
                table: "tbl_district_wise_lightening");

            migrationBuilder.CreateTable(
                name: "tbl_district_wise_thunderstorm",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", nullable: true),
                    latitude = table.Column<decimal>(type: "decimal(10, 8)", nullable: true),
                    longitude = table.Column<decimal>(type: "decimal(10, 8)", nullable: true),
                    thunderstorm = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_district_wise_thunderstorm", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_district_wise_thunderstorm");

            migrationBuilder.AddColumn<int>(
                name: "thunderstm",
                table: "tbl_district_wise_lightening",
                type: "int",
                nullable: true);
        }
    }
}
