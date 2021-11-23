using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace EcdsApp.Migrations
{
    public partial class Update_ThemeModels : Migration
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
                name: "lkp_theme_layer_types",
                columns: table => new
                {
                    layer_type_id = table.Column<int>(type: "int", nullable: false),
                    Layer_type_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_theme_layer_types", x => x.layer_type_id);
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
                    main_attribute_display_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    main_attribute_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    main_attribute_code = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    first_attribute_display_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    first_attribute_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    first_attribute_code = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    second_attribute_display_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    second_attribute_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    second_attribute_code = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    third_attribute_display_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    third_attribute_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    third_attribute_code = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    file_lat_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    file_long_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
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
                        name: "FK_tbl_theme_layer_details_lkp_theme_layer_types_layer_type_id",
                        column: x => x.layer_type_id,
                        principalTable: "lkp_theme_layer_types",
                        principalColumn: "layer_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_layer_legend_colors",
                columns: table => new
                {
                    layer_legend_color_id = table.Column<int>(type: "int", nullable: false),
                    layer_id = table.Column<int>(type: "int", nullable: false),
                    layer_main_attribure_value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    layer_legend_color_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_layer_legend_colors", x => x.layer_legend_color_id);
                    table.ForeignKey(
                        name: "FK_tbl_layer_legend_colors_tbl_theme_layer_details_layer_id",
                        column: x => x.layer_id,
                        principalTable: "tbl_theme_layer_details",
                        principalColumn: "layer_id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_lkp_sub_themes_theme_id",
                table: "lkp_sub_themes",
                column: "theme_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_layer_legend_colors_layer_id",
                table: "tbl_layer_legend_colors",
                column: "layer_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_theme_layer_details_layer_type_id",
                table: "tbl_theme_layer_details",
                column: "layer_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_theme_layer_details_sub_theme_id",
                table: "tbl_theme_layer_details",
                column: "sub_theme_id");

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
                name: "tbl_layer_legend_colors");

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
                name: "tbl_theme_layer_details");

            migrationBuilder.DropTable(
                name: "user_role_lists");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "lkp_admin_boundary_divisions");

            migrationBuilder.DropTable(
                name: "lkp_sub_themes");

            migrationBuilder.DropTable(
                name: "lkp_theme_layer_types");

            migrationBuilder.DropTable(
                name: "lkp_themes");
        }
    }
}
