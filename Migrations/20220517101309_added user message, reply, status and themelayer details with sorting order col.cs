using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace EcdsApp.Migrations
{
    public partial class addedusermessagereplystatusandthemelayerdetailswithsortingordercol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sorting_order",
                table: "tbl_theme_layer_details",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_reply_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    message_status = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_reply_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user_message",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    full_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    message = table.Column<string>(type: "varchar(1000)", nullable: false),
                    reply_status = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user_message", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_user_message_tbl_reply_statuses_reply_status",
                        column: x => x.reply_status,
                        principalTable: "tbl_reply_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_message_replys",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    replied_message = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    message = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_message_replys", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_message_replys_tbl_user_message_message",
                        column: x => x.message,
                        principalTable: "tbl_user_message",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_message_replys_message",
                table: "tbl_message_replys",
                column: "message");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_user_message_reply_status",
                table: "tbl_user_message",
                column: "reply_status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_message_replys");

            migrationBuilder.DropTable(
                name: "tbl_user_message");

            migrationBuilder.DropTable(
                name: "tbl_reply_statuses");

            migrationBuilder.DropColumn(
                name: "sorting_order",
                table: "tbl_theme_layer_details");
        }
    }
}
