using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class add3divisionalmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "household_affected_and_got_early_warning_from_2015_to_2020",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    div_geo_code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    affected_household = table.Column<int>(type: "int", nullable: false),
                    got_early_warning = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_affected_and_got_early_warning_from_2015_to_2020", x => x.id);
                    table.ForeignKey(
                        name: "FK_household_affected_and_got_early_warning_from_2015_to_2020_l~",
                        column: x => x.div_geo_code,
                        principalTable: "lkp_admin_boundary_divisions",
                        principalColumn: "div_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "household_affected_by_non_working_days",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    div_geo_code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    day_1_to_7 = table.Column<int>(type: "int", nullable: false),
                    day_8_to_15 = table.Column<int>(type: "int", nullable: false),
                    day_16_to_30 = table.Column<int>(type: "int", nullable: false),
                    day_31_to_45 = table.Column<int>(type: "int", nullable: false),
                    day_46_to_60 = table.Column<int>(type: "int", nullable: false),
                    day_61_plus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_affected_by_non_working_days", x => x.id);
                    table.ForeignKey(
                        name: "FK_household_affected_by_non_working_days_lkp_admin_boundary_di~",
                        column: x => x.div_geo_code,
                        principalTable: "lkp_admin_boundary_divisions",
                        principalColumn: "div_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "household_got_early_warning_by_type_of_media",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    div_geo_code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    radio = table.Column<int>(type: "int", nullable: false),
                    television = table.Column<int>(type: "int", nullable: false),
                    making = table.Column<int>(type: "int", nullable: false),
                    community = table.Column<int>(type: "int", nullable: false),
                    local_administration = table.Column<int>(type: "int", nullable: false),
                    mobile_telephone_or_sms = table.Column<int>(type: "int", nullable: false),
                    internet_media = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_got_early_warning_by_type_of_media", x => x.id);
                    table.ForeignKey(
                        name: "FK_household_got_early_warning_by_type_of_media_lkp_admin_bound~",
                        column: x => x.div_geo_code,
                        principalTable: "lkp_admin_boundary_divisions",
                        principalColumn: "div_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_household_affected_and_got_early_warning_from_2015_to_2020_d~",
                table: "household_affected_and_got_early_warning_from_2015_to_2020",
                column: "div_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_household_affected_by_non_working_days_div_geo_code",
                table: "household_affected_by_non_working_days",
                column: "div_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_household_got_early_warning_by_type_of_media_div_geo_code",
                table: "household_got_early_warning_by_type_of_media",
                column: "div_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "household_affected_and_got_early_warning_from_2015_to_2020");

            migrationBuilder.DropTable(
                name: "household_affected_by_non_working_days");

            migrationBuilder.DropTable(
                name: "household_got_early_warning_by_type_of_media");
        }
    }
}
