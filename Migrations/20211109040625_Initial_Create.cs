using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace EcdsApp.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lkp_admin_boundary_divisions",
                columns: table => new
                {
                    div_geo_code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false),
                    div_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    div_name_bangla = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    sorting_order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_admin_boundary_divisions", x => x.div_geo_code);
                });

            migrationBuilder.CreateTable(
                name: "user_role_lists",
                columns: table => new
                {
                    role_id = table.Column<string>(type: "varchar(256)", nullable: false),
                    name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role_lists", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "varchar(256)", nullable: false),
                    user_activation_status_id = table.Column<int>(type: "int", nullable: true),
                    date_of_creation = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    verification_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    user_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    access_failed_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_admin_boundary_districts",
                columns: table => new
                {
                    dist_geo_code = table.Column<string>(type: "varchar(4)", nullable: false),
                    dist_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    dist_name_bangla = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    div_geo_code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    sorting_order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_admin_boundary_districts", x => x.dist_geo_code);
                    table.ForeignKey(
                        name: "FK_lkp_admin_boundary_districts_lkp_admin_boundary_divisions_di~",
                        column: x => x.div_geo_code,
                        principalTable: "lkp_admin_boundary_divisions",
                        principalColumn: "div_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<string>(type: "varchar(256)", nullable: false),
                    claim_yype = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role_claims_user_role_lists_role_id",
                        column: x => x.role_id,
                        principalTable: "user_role_lists",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<string>(type: "varchar(256)", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_claims_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "varchar(256)", nullable: false),
                    provider_key = table.Column<string>(type: "varchar(256)", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "FK_user_logins_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "varchar(256)", nullable: false),
                    role_id = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_user_roles_user_role_lists_role_id",
                        column: x => x.role_id,
                        principalTable: "user_role_lists",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "varchar(256)", nullable: false),
                    login_provider = table.Column<string>(type: "varchar(256)", nullable: false),
                    name = table.Column<string>(type: "varchar(256)", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "FK_user_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lkp_admin_boundary_upazilas",
                columns: table => new
                {
                    upz_geo_code = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    upz_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    upz_name_bangla = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    sorting_order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_admin_boundary_upazilas", x => x.upz_geo_code);
                    table.ForeignKey(
                        name: "FK_lkp_admin_boundary_upazilas_lkp_admin_boundary_districts_dis~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lkp_admin_boundary_districts_div_geo_code",
                table: "lkp_admin_boundary_districts",
                column: "div_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_lkp_admin_boundary_upazilas_dist_geo_code",
                table: "lkp_admin_boundary_upazilas",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_user_claims_user_id",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_logins_user_id",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_claims_role_id",
                table: "user_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "user_role_lists",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "normalized_user_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lkp_admin_boundary_upazilas");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_role_claims");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "lkp_admin_boundary_districts");

            migrationBuilder.DropTable(
                name: "user_role_lists");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "lkp_admin_boundary_divisions");
        }
    }
}
