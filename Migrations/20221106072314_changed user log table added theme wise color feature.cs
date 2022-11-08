using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class changeduserlogtableaddedthemewisecolorfeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_UserLogs_LogTypes_log_type_id",
            //     table: "UserLogs");
            
            // migrationBuilder.DropColumn(
            //     name: "log_type_id",
            //     table: "UserLogs"
            //     );
            // migrationBuilder.AddColumn<int>(
            //     name: "theme_layer_id",
            //     table: "UserLogs",
            //     type: "int",
            //     nullable:false);
            // migrationBuilder.RenameColumn(
            //     name: "log_type_id",
            //     table: "UserLogs",
            //     newName: "theme_layer_id");

            // migrationBuilder.CreateIndex(
            //     name: "IX_UserLogs_theme_layer_id",
            //     table: "UserLogs",
            //     column:"theme_layer_id");
            //
            // migrationBuilder.AddForeignKey(
            //     name: "FK_UserLogs_tbl_theme_layer_details_theme_layer_id",
            //     table: "UserLogs",
            //     column: "theme_layer_id",
            //     principalTable: "tbl_theme_layer_details",
            //     principalColumn: "layer_id",
            //     onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_UserLogs_tbl_theme_layer_details_theme_layer_id",
            //     table: "UserLogs");
            //
            // migrationBuilder.DropColumn(
            //     name: "theme_layer_id",
            //     table: "UserLogs"
            // );
            // migrationBuilder.AddColumn<int>(
            //     name: "log_type_id",
            //     table: "UserLogs",
            //     type: "int",
            //     nullable:false);
            //
            // migrationBuilder.DropIndex(
            //     name: "IX_UserLogs_theme_layer_id",
            //     table: "UserLogs");
            //
            // migrationBuilder.AddForeignKey(
            //     name: "FK_UserLogs_LogTypes_log_type_id",
            //     table: "UserLogs",
            //     column: "log_type_id",
            //     principalTable: "LogTypes",
            //     principalColumn: "id",
            //     onDelete: ReferentialAction.Cascade);
        }
    }
}
