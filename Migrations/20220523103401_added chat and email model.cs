using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace EcdsApp.Migrations
{
    public partial class addedchatandemailmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    sender_email = table.Column<string>(type: "varchar(150)", nullable: false),
                    message = table.Column<string>(type: "varchar(500)", nullable: false),
                    receiver_email = table.Column<string>(type: "varchar(150)", nullable: false),
                    sent_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "email_configurations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    sender_email = table.Column<string>(type: "varchar(150)", nullable: false),
                    category = table.Column<string>(type: "varchar(150)", nullable: false),
                    host = table.Column<string>(type: "varchar(150)", nullable: false),
                    port = table.Column<string>(type: "varchar(150)", nullable: false),
                    username = table.Column<string>(type: "varchar(150)", nullable: false),
                    password = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_configurations", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chats_receiver_email",
                table: "chats",
                column: "receiver_email");

            migrationBuilder.CreateIndex(
                name: "IX_chats_sender_email",
                table: "chats",
                column: "sender_email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "email_configurations");
        }
    }
}
