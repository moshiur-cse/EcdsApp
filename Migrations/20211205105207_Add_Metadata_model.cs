using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class Add_Metadata_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "layer_legend_display_name",
                table: "tbl_layer_legend_colors",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_metadata_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    layer_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    @abstract = table.Column<string>(name: "abstract", type: "varchar(1000)", maxLength: 1000, nullable: false),
                    general = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    quality = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    completeness = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    history_of_the_dataset = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    purpose_of_production = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    process_description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    type_of_dataset = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    dataset_language = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    additional_info_source_for_dataset = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_metadata_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_metadata_details_tbl_theme_layer_details_layer_id",
                        column: x => x.layer_id,
                        principalTable: "tbl_theme_layer_details",
                        principalColumn: "layer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_metadata_details_layer_id",
                table: "tbl_metadata_details",
                column: "layer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_metadata_details");

            migrationBuilder.AlterColumn<string>(
                name: "layer_legend_display_name",
                table: "tbl_layer_legend_colors",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
