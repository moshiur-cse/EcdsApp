using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class AddDistrictModelsNgoGovt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "household_received_rehabilitation_loan_by_govt_org",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    Support_failure_during_or_after_disaster = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_during_or_after_disaster = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    local_welfare_or_corporate_support = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_business_enterprise = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_local_persons = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_int_orgs = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_ngos = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_gov_orgs = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_others = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_received_rehabilitation_loan_by_govt_org", x => x.id);
                    table.ForeignKey(
                        name: "FK_household_received_rehabilitation_loan_by_govt_org_lkp_admin~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "household_received_rehabilitation_loan_by_ngo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    Support_failure_during_or_after_disaster = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_during_or_after_disaster = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    local_welfare_or_corporate_support = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_business_enterprise = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_local_persons = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_int_orgs = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_ngos = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_gov_orgs = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    support_from_others = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_received_rehabilitation_loan_by_ngo", x => x.id);
                    table.ForeignKey(
                        name: "FK_household_received_rehabilitation_loan_by_ngo_lkp_admin_boun~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_household_received_rehabilitation_loan_by_govt_org_dist_geo_~",
                table: "household_received_rehabilitation_loan_by_govt_org",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_household_received_rehabilitation_loan_by_ngo_dist_geo_code",
                table: "household_received_rehabilitation_loan_by_ngo",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "household_received_rehabilitation_loan_by_govt_org");

            migrationBuilder.DropTable(
                name: "household_received_rehabilitation_loan_by_ngo");
        }
    }
}
