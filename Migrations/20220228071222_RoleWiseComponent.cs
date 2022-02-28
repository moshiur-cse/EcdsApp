using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class RoleWiseComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_role_wise_components",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<string>(type: "varchar(767)", nullable: true),
                    component_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role_wise_components", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role_wise_components_lkp_sub_themes_component_id",
                        column: x => x.component_id,
                        principalTable: "lkp_sub_themes",
                        principalColumn: "sub_theme_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_wise_components_user_role_lists_role_id",
                        column: x => x.role_id,
                        principalTable: "user_role_lists",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_role_wise_components_component_id",
                table: "user_role_wise_components",
                column: "component_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_wise_components_role_id",
                table: "user_role_wise_components",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_role_wise_components");
        }
    }
}
