using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class add_layer_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lkp_theme_layer_type",
                columns: table => new
                {
                    layer_type_id = table.Column<int>(type: "int", nullable: false),
                    Layer_type_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_theme_layer_type", x => x.layer_type_id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_themes",
                columns: table => new
                {
                    theme_id = table.Column<int>(type: "int", nullable: false),
                    theme_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    theme_path = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_themes", x => x.theme_id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_sub_themes",
                columns: table => new
                {
                    sub_theme_id = table.Column<int>(type: "int", nullable: false),
                    theme_id = table.Column<int>(type: "int", nullable: false),
                    sub_theme_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    sub_theme_path = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_sub_themes", x => x.sub_theme_id);
                    table.ForeignKey(
                        name: "FK_lkp_sub_themes_lkp_themes_theme_id",
                        column: x => x.theme_id,
                        principalTable: "lkp_themes",
                        principalColumn: "theme_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_theme_layer_details",
                columns: table => new
                {
                    layer_id = table.Column<int>(type: "int", nullable: false),
                    sub_theme_id = table.Column<int>(type: "int", nullable: false),
                    layer_path = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    layer_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    layer_file_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    layer_type_id = table.Column<int>(type: "int", nullable: false),
                    is_legend_color = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_theme_layer_details", x => x.layer_id);
                    table.ForeignKey(
                        name: "FK_tbl_theme_layer_details_lkp_sub_themes_sub_theme_id",
                        column: x => x.sub_theme_id,
                        principalTable: "lkp_sub_themes",
                        principalColumn: "sub_theme_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_theme_layer_details_lkp_theme_layer_type_layer_type_id",
                        column: x => x.layer_type_id,
                        principalTable: "lkp_theme_layer_type",
                        principalColumn: "layer_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_layer_legend_color",
                columns: table => new
                {
                    layer_legend_color_id = table.Column<int>(type: "int", nullable: false),
                    layer_id = table.Column<int>(type: "int", nullable: false),
                    ThemeLayerDetailsLayerId = table.Column<int>(type: "int", nullable: true),
                    layer_legend_color_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_layer_legend_color", x => x.layer_legend_color_id);
                    table.ForeignKey(
                        name: "FK_tbl_layer_legend_color_tbl_theme_layer_details_ThemeLayerDet~",
                        column: x => x.ThemeLayerDetailsLayerId,
                        principalTable: "tbl_theme_layer_details",
                        principalColumn: "layer_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lkp_sub_themes_theme_id",
                table: "lkp_sub_themes",
                column: "theme_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_layer_legend_color_ThemeLayerDetailsLayerId",
                table: "tbl_layer_legend_color",
                column: "ThemeLayerDetailsLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_theme_layer_details_layer_type_id",
                table: "tbl_theme_layer_details",
                column: "layer_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_theme_layer_details_sub_theme_id",
                table: "tbl_theme_layer_details",
                column: "sub_theme_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_layer_legend_color");

            migrationBuilder.DropTable(
                name: "tbl_theme_layer_details");

            migrationBuilder.DropTable(
                name: "lkp_sub_themes");

            migrationBuilder.DropTable(
                name: "lkp_theme_layer_type");

            migrationBuilder.DropTable(
                name: "lkp_themes");
        }
    }
}
