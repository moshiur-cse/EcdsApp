using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class Adddistrictmodels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "household_received_loan_by_10k_plus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    tk_1_to_9999 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    tk_10k_to_24999 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    tk_25k_to_49999 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    tk_50k_to_99999 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_received_loan_by_10k_plus", x => x.id);
                    table.ForeignKey(
                        name: "FK_household_received_loan_by_10k_plus_lkp_admin_boundary_distr~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "household_received_loan_by_50k_to_10k",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    tk_1_to_9999 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    tk_10k_to_24999 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    tk_25k_to_49999 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    tk_50k_to_99999 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_received_loan_by_50k_to_10k", x => x.id);
                    table.ForeignKey(
                        name: "FK_household_received_loan_by_50k_to_10k_lkp_admin_boundary_dis~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "respondent_received_loan_govt_bank",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    not_received_loan = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    local_society = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    welfare_cooparative_society = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    local_mahajan = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    relatives = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    private_micro_loan = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    govt_bank = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    private_bank = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    others = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respondent_received_loan_govt_bank", x => x.id);
                    table.ForeignKey(
                        name: "FK_respondent_received_loan_govt_bank_lkp_admin_boundary_distri~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "respondent_received_loan_private_bank",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    not_received_loan = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    local_society = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    welfare_cooparative_society = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    local_mahajan = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    relatives = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    private_micro_loan = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    govt_bank = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    private_bank = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    others = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respondent_received_loan_private_bank", x => x.id);
                    table.ForeignKey(
                        name: "FK_respondent_received_loan_private_bank_lkp_admin_boundary_dis~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_household_received_loan_by_10k_plus_dist_geo_code",
                table: "household_received_loan_by_10k_plus",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_household_received_loan_by_50k_to_10k_dist_geo_code",
                table: "household_received_loan_by_50k_to_10k",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_respondent_received_loan_govt_bank_dist_geo_code",
                table: "respondent_received_loan_govt_bank",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_respondent_received_loan_private_bank_dist_geo_code",
                table: "respondent_received_loan_private_bank",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "household_received_loan_by_10k_plus");

            migrationBuilder.DropTable(
                name: "household_received_loan_by_50k_to_10k");

            migrationBuilder.DropTable(
                name: "respondent_received_loan_govt_bank");

            migrationBuilder.DropTable(
                name: "respondent_received_loan_private_bank");
        }
    }
}
