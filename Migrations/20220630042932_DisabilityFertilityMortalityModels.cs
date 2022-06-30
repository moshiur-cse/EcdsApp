using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class DisabilityFertilityMortalityModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "district_wise_disability_disab_rate",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    year_2016 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2017 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2018 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2019 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2020 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district_wise_disability_disab_rate", x => x.id);
                    table.ForeignKey(
                        name: "FK_district_wise_disability_disab_rate_lkp_admin_boundary_distr~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "district_wise_fertility_rate_cbr",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    year_2016 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2017 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2018 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2019 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2020 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district_wise_fertility_rate_cbr", x => x.id);
                    table.ForeignKey(
                        name: "FK_district_wise_fertility_rate_cbr_lkp_admin_boundary_district~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "district_wise_fertility_rate_cpr",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    year_2016 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2017 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2018 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2019 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2020 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district_wise_fertility_rate_cpr", x => x.id);
                    table.ForeignKey(
                        name: "FK_district_wise_fertility_rate_cpr_lkp_admin_boundary_district~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "district_wise_fertility_rate_gfr",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    year_2016 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2017 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2018 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2019 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2020 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district_wise_fertility_rate_gfr", x => x.id);
                    table.ForeignKey(
                        name: "FK_district_wise_fertility_rate_gfr_lkp_admin_boundary_district~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "district_wise_fertility_rate_tfr",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    year_2016 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2017 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2018 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2019 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2020 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district_wise_fertility_rate_tfr", x => x.id);
                    table.ForeignKey(
                        name: "FK_district_wise_fertility_rate_tfr_lkp_admin_boundary_district~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "district_wise_mortality_rate_cdr",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    year_2016 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2017 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2018 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2019 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2020 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district_wise_mortality_rate_cdr", x => x.id);
                    table.ForeignKey(
                        name: "FK_district_wise_mortality_rate_cdr_lkp_admin_boundary_district~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "district_wise_mortality_rate_imr",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    year_2016 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2017 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2018 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2019 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2020 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district_wise_mortality_rate_imr", x => x.id);
                    table.ForeignKey(
                        name: "FK_district_wise_mortality_rate_imr_lkp_admin_boundary_district~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "district_wise_mortality_rate_u5mr",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    year_2016 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2017 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2018 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2019 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2020 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district_wise_mortality_rate_u5mr", x => x.id);
                    table.ForeignKey(
                        name: "FK_district_wise_mortality_rate_u5mr_lkp_admin_boundary_distric~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_district_wise_disability_disab_rate_dist_geo_code",
                table: "district_wise_disability_disab_rate",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_district_wise_fertility_rate_cbr_dist_geo_code",
                table: "district_wise_fertility_rate_cbr",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_district_wise_fertility_rate_cpr_dist_geo_code",
                table: "district_wise_fertility_rate_cpr",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_district_wise_fertility_rate_gfr_dist_geo_code",
                table: "district_wise_fertility_rate_gfr",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_district_wise_fertility_rate_tfr_dist_geo_code",
                table: "district_wise_fertility_rate_tfr",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_district_wise_mortality_rate_cdr_dist_geo_code",
                table: "district_wise_mortality_rate_cdr",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_district_wise_mortality_rate_imr_dist_geo_code",
                table: "district_wise_mortality_rate_imr",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_district_wise_mortality_rate_u5mr_dist_geo_code",
                table: "district_wise_mortality_rate_u5mr",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "district_wise_disability_disab_rate");

            migrationBuilder.DropTable(
                name: "district_wise_fertility_rate_cbr");

            migrationBuilder.DropTable(
                name: "district_wise_fertility_rate_cpr");

            migrationBuilder.DropTable(
                name: "district_wise_fertility_rate_gfr");

            migrationBuilder.DropTable(
                name: "district_wise_fertility_rate_tfr");

            migrationBuilder.DropTable(
                name: "district_wise_mortality_rate_cdr");

            migrationBuilder.DropTable(
                name: "district_wise_mortality_rate_imr");

            migrationBuilder.DropTable(
                name: "district_wise_mortality_rate_u5mr");
        }
    }
}
