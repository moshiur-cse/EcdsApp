using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class RoleWisePermittedContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_futute_projection_rainfall_4_point_5");

            migrationBuilder.CreateTable(
                name: "user_permitted_contents",
                columns: table => new
                {
                    content_id = table.Column<int>(type: "int", nullable: false),
                    menu_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    controller_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    action_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    submenu_content = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_permitted_contents", x => x.content_id);
                });

            migrationBuilder.CreateTable(
                name: "user_role_wise_permitted_contents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<string>(type: "varchar(767)", nullable: true),
                    content_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role_wise_permitted_contents", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role_wise_permitted_contents_user_permitted_contents_co~",
                        column: x => x.content_id,
                        principalTable: "user_permitted_contents",
                        principalColumn: "content_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_wise_permitted_contents_user_role_lists_role_id",
                        column: x => x.role_id,
                        principalTable: "user_role_lists",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_role_wise_permitted_contents_content_id",
                table: "user_role_wise_permitted_contents",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_wise_permitted_contents_role_id",
                table: "user_role_wise_permitted_contents",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_role_wise_permitted_contents");

            migrationBuilder.DropTable(
                name: "user_permitted_contents");

            migrationBuilder.CreateTable(
                name: "tbl_futute_projection_rainfall_4_point_5",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    union_geo_code = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    year_2020_2039 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2040_2059 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2060_2079 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    year_2080_2099 = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_futute_projection_rainfall_4_point_5", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_futute_projection_rainfall_4_point_5_lkp_admin_boundary_~",
                        column: x => x.union_geo_code,
                        principalTable: "lkp_admin_boundary_unions",
                        principalColumn: "union_geo_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_futute_projection_rainfall_4_point_5_union_geo_code",
                table: "tbl_futute_projection_rainfall_4_point_5",
                column: "union_geo_code");
        }
    }
}
