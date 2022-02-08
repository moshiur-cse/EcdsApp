using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class UserAccessPerContentsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_access_modules",
                columns: table => new
                {
                    module_id = table.Column<int>(type: "int", nullable: false),
                    module_name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_access_modules", x => x.module_id);
                });

            migrationBuilder.CreateTable(
                name: "user_permitted_contents",
                columns: table => new
                {
                    content_id = table.Column<int>(type: "int", nullable: false),
                    theme_id = table.Column<string>(type: "text", nullable: true),
                    sub_theme_id = table.Column<string>(type: "text", nullable: false),
                    layer_id = table.Column<string>(type: "text", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    menu_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    submenu_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    controller_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    action_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    submenu_content = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_menu_item = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_disabled = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_permitted_contents", x => x.content_id);
                    table.ForeignKey(
                        name: "FK_user_permitted_contents_user_access_modules_module_id",
                        column: x => x.module_id,
                        principalTable: "user_access_modules",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_permitted_contents_module_id",
                table: "user_permitted_contents",
                column: "module_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_permitted_contents");

            migrationBuilder.DropTable(
                name: "user_access_modules");
        }
    }
}
