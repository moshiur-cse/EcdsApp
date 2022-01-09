using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class TableColumnExposure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "div_name_bangla",
                table: "lkp_admin_boundary_divisions",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "div_name",
                table: "lkp_admin_boundary_divisions",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.CreateTable(
                name: "lkp_exposure_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    category_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_exposure_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_table_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    sub_theme_id = table.Column<int>(type: "int", nullable: false),
                    table_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    table_model_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    display_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_table_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_table_info_lkp_sub_themes_sub_theme_id",
                        column: x => x.sub_theme_id,
                        principalTable: "lkp_sub_themes",
                        principalColumn: "sub_theme_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_upazila_wise_exposure_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    upz_geo_code = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    flood_value = table.Column<int>(type: "int", nullable: true),
                    storm_surge_value = table.Column<int>(type: "int", nullable: true),
                    land_slide_value = table.Column<int>(type: "int", nullable: true),
                    drought_value = table.Column<int>(type: "int", nullable: true),
                    earthquake_value = table.Column<int>(type: "int", nullable: true),
                    tsunami_value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_upazila_wise_exposure_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_upazila_wise_exposure_data_lkp_admin_boundary_upazilas_u~",
                        column: x => x.upz_geo_code,
                        principalTable: "lkp_admin_boundary_upazilas",
                        principalColumn: "upz_geo_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_upazila_wise_exposure_data_lkp_exposure_category_drought~",
                        column: x => x.drought_value,
                        principalTable: "lkp_exposure_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_upazila_wise_exposure_data_lkp_exposure_category_earthqu~",
                        column: x => x.earthquake_value,
                        principalTable: "lkp_exposure_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_upazila_wise_exposure_data_lkp_exposure_category_flood_v~",
                        column: x => x.flood_value,
                        principalTable: "lkp_exposure_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_upazila_wise_exposure_data_lkp_exposure_category_land_sl~",
                        column: x => x.land_slide_value,
                        principalTable: "lkp_exposure_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_upazila_wise_exposure_data_lkp_exposure_category_storm_s~",
                        column: x => x.storm_surge_value,
                        principalTable: "lkp_exposure_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_upazila_wise_exposure_data_lkp_exposure_category_tsunami~",
                        column: x => x.tsunami_value,
                        principalTable: "lkp_exposure_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_table_column_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    table_id = table.Column<int>(type: "int", nullable: false),
                    db_column_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    model_property_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    display_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_table_column_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_table_column_info_tbl_table_info_table_id",
                        column: x => x.table_id,
                        principalTable: "tbl_table_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_table_column_info_table_id",
                table: "tbl_table_column_info",
                column: "table_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_table_info_sub_theme_id",
                table: "tbl_table_info",
                column: "sub_theme_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_upazila_wise_exposure_data_drought_value",
                table: "tbl_upazila_wise_exposure_data",
                column: "drought_value");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_upazila_wise_exposure_data_earthquake_value",
                table: "tbl_upazila_wise_exposure_data",
                column: "earthquake_value");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_upazila_wise_exposure_data_flood_value",
                table: "tbl_upazila_wise_exposure_data",
                column: "flood_value");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_upazila_wise_exposure_data_land_slide_value",
                table: "tbl_upazila_wise_exposure_data",
                column: "land_slide_value");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_upazila_wise_exposure_data_storm_surge_value",
                table: "tbl_upazila_wise_exposure_data",
                column: "storm_surge_value");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_upazila_wise_exposure_data_tsunami_value",
                table: "tbl_upazila_wise_exposure_data",
                column: "tsunami_value");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_upazila_wise_exposure_data_upz_geo_code",
                table: "tbl_upazila_wise_exposure_data",
                column: "upz_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_table_column_info");

            migrationBuilder.DropTable(
                name: "tbl_upazila_wise_exposure_data");

            migrationBuilder.DropTable(
                name: "tbl_table_info");

            migrationBuilder.DropTable(
                name: "lkp_exposure_category");

            migrationBuilder.AlterColumn<string>(
                name: "div_name_bangla",
                table: "lkp_admin_boundary_divisions",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "div_name",
                table: "lkp_admin_boundary_divisions",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250);
        }
    }
}
