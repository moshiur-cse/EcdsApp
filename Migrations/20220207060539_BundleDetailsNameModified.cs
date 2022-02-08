using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class BundleDetailsNameModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_bundle_detatils");

            migrationBuilder.CreateTable(
                name: "tbl_bundle_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    layer_id = table.Column<int>(type: "int", nullable: false),
                    field_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    field_description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    field_unit = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_bundle_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_bundle_details_tbl_theme_layer_details_layer_id",
                        column: x => x.layer_id,
                        principalTable: "tbl_theme_layer_details",
                        principalColumn: "layer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_bundle_details_layer_id",
                table: "tbl_bundle_details",
                column: "layer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_bundle_details");

            migrationBuilder.CreateTable(
                name: "tbl_bundle_detatils",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    field_description = table.Column<string>(type: "varchar(500)", maxLength: 1000, nullable: false),
                    field_name = table.Column<string>(type: "varchar(200)", maxLength: 1000, nullable: false),
                    field_unit = table.Column<string>(type: "varchar(50)", maxLength: 1000, nullable: true),
                    layer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_bundle_detatils", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_bundle_detatils_tbl_theme_layer_details_layer_id",
                        column: x => x.layer_id,
                        principalTable: "tbl_theme_layer_details",
                        principalColumn: "layer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_bundle_detatils_layer_id",
                table: "tbl_bundle_detatils",
                column: "layer_id");
        }
    }
}
