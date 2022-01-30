using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class LayerNameAndDvStateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "layer_path",
                table: "tbl_theme_layer_details");

            migrationBuilder.AlterColumn<string>(
                name: "layer_name",
                table: "tbl_theme_layer_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<int>(
                name: "data_verification_state",
                table: "tbl_theme_layer_details",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "layer_display_name",
                table: "tbl_theme_layer_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "lkp_data_verification_states",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    state_name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_data_verification_states", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_theme_layer_details_data_verification_state",
                table: "tbl_theme_layer_details",
                column: "data_verification_state");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_theme_layer_details_lkp_data_verification_states_data_ve~",
                table: "tbl_theme_layer_details",
                column: "data_verification_state",
                principalTable: "lkp_data_verification_states",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_theme_layer_details_lkp_data_verification_states_data_ve~",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropTable(
                name: "lkp_data_verification_states");

            migrationBuilder.DropIndex(
                name: "IX_tbl_theme_layer_details_data_verification_state",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "data_verification_state",
                table: "tbl_theme_layer_details");

            migrationBuilder.DropColumn(
                name: "layer_display_name",
                table: "tbl_theme_layer_details");

            migrationBuilder.AlterColumn<string>(
                name: "layer_name",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "layer_path",
                table: "tbl_theme_layer_details",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
