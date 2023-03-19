using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class UpdateRespondentPerceptionAboutImpactOfClimateChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "do_not_know",
                table: "respondent_perception_about_impact_climate_cng",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "do_not_know",
                table: "respondent_perception_about_impact_climate_cng");
        }
    }
}
