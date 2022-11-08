using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace EcdsApp.Migrations
{
    public partial class addeddownloadloginsystemanddeleteduserlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.CreateTable(
                name: "download_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<string>(type: "varchar(767)", nullable: true),
                    ip_address = table.Column<string>(type: "varchar(100)", nullable: false),
                    generated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    theme_layer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_download_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_download_log_tbl_theme_layer_details_theme_layer_id",
                        column: x => x.theme_layer_id,
                        principalTable: "tbl_theme_layer_details",
                        principalColumn: "layer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_download_log_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_download_log_theme_layer_id",
                table: "download_log",
                column: "theme_layer_id");

            migrationBuilder.CreateIndex(
                name: "IX_download_log_user_id",
                table: "download_log",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "download_log");

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    generated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    ip_address = table.Column<string>(type: "varchar(100)", nullable: false),
                    theme_layer_id = table.Column<int>(type: "int", nullable: false),
                    user_email = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserLogs_tbl_theme_layer_details_theme_layer_id",
                        column: x => x.theme_layer_id,
                        principalTable: "tbl_theme_layer_details",
                        principalColumn: "layer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLogs_users_user_email",
                        column: x => x.user_email,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_theme_layer_id",
                table: "UserLogs",
                column: "theme_layer_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_user_email",
                table: "UserLogs",
                column: "user_email");
        }
    }
}
