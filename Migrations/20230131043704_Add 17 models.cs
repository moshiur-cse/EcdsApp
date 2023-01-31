using Microsoft.EntityFrameworkCore.Migrations;

namespace EcdsApp.Migrations
{
    public partial class Add17models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bfor_dis_sufferings_according_to_cause_of_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    temperature_variation = table.Column<int>(type: "int", nullable: false),
                    variation_in_rain = table.Column<int>(type: "int", nullable: false),
                    water_pollution = table.Column<int>(type: "int", nullable: false),
                    air_pollution = table.Column<int>(type: "int", nullable: false),
                    unplanned_sanitation = table.Column<int>(type: "int", nullable: false),
                    during_disaster = table.Column<int>(type: "int", nullable: false),
                    not_known = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bfor_dis_sufferings_according_to_cause_of_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_bfor_dis_sufferings_according_to_cause_of_disease_lkp_admin_~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bfor_dis_sufferings_according_to_type_of_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    diarrhoea = table.Column<int>(type: "int", nullable: false),
                    dysentery = table.Column<int>(type: "int", nullable: false),
                    malaria = table.Column<int>(type: "int", nullable: false),
                    skin_disease = table.Column<int>(type: "int", nullable: false),
                    cold_or_cough = table.Column<int>(type: "int", nullable: false),
                    fever = table.Column<int>(type: "int", nullable: false),
                    typhoid = table.Column<int>(type: "int", nullable: false),
                    asthma = table.Column<int>(type: "int", nullable: false),
                    jaundice = table.Column<int>(type: "int", nullable: false),
                    malnutrition_related = table.Column<int>(type: "int", nullable: false),
                    dengue = table.Column<int>(type: "int", nullable: false),
                    chikungunia = table.Column<int>(type: "int", nullable: false),
                    mental_disorder = table.Column<int>(type: "int", nullable: false),
                    chicken_pox = table.Column<int>(type: "int", nullable: false),
                    cholera = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bfor_dis_sufferings_according_to_type_of_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_bfor_dis_sufferings_according_to_type_of_disease_lkp_admin_b~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "domestic_water_by_source_bfr_disaster",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    pipe_or_tap = table.Column<int>(type: "int", nullable: false),
                    shallow_tube_well_199_ft = table.Column<int>(type: "int", nullable: false),
                    deep_tube_well_200_ft_or_more = table.Column<int>(type: "int", nullable: false),
                    pond_or_dighi = table.Column<int>(type: "int", nullable: false),
                    canel_or_river = table.Column<int>(type: "int", nullable: false),
                    rainfall_or_water_fall = table.Column<int>(type: "int", nullable: false),
                    well = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_domestic_water_by_source_bfr_disaster", x => x.id);
                    table.ForeignKey(
                        name: "FK_domestic_water_by_source_bfr_disaster_lkp_admin_boundary_dis~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "domestic_water_by_source_dur_disaster",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    pipe_or_tap = table.Column<int>(type: "int", nullable: false),
                    shallow_tube_well_199_ft = table.Column<int>(type: "int", nullable: false),
                    deep_tube_well_200_ft_or_more = table.Column<int>(type: "int", nullable: false),
                    pond_or_dighi = table.Column<int>(type: "int", nullable: false),
                    canel_or_river = table.Column<int>(type: "int", nullable: false),
                    rainfall_or_water_fall = table.Column<int>(type: "int", nullable: false),
                    well = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_domestic_water_by_source_dur_disaster", x => x.id);
                    table.ForeignKey(
                        name: "FK_domestic_water_by_source_dur_disaster_lkp_admin_boundary_dis~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "drinking_water_by_source_dur_disaster",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    pipe_or_tap = table.Column<int>(type: "int", nullable: false),
                    shallow_tube_well_199_ft = table.Column<int>(type: "int", nullable: false),
                    deep_tube_well_200_ft_or_more = table.Column<int>(type: "int", nullable: false),
                    pond_or_dighi = table.Column<int>(type: "int", nullable: false),
                    canel_or_river = table.Column<int>(type: "int", nullable: false),
                    rainfall_or_water_fall = table.Column<int>(type: "int", nullable: false),
                    well = table.Column<int>(type: "int", nullable: false),
                    bottle_water = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drinking_water_by_source_dur_disaster", x => x.id);
                    table.ForeignKey(
                        name: "FK_drinking_water_by_source_dur_disaster_lkp_admin_boundary_dis~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dur_dis_sufferings_according_to_cause_of_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    temperature_variation = table.Column<int>(type: "int", nullable: false),
                    variation_in_rain = table.Column<int>(type: "int", nullable: false),
                    water_pollution = table.Column<int>(type: "int", nullable: false),
                    air_pollution = table.Column<int>(type: "int", nullable: false),
                    unplanned_sanitation = table.Column<int>(type: "int", nullable: false),
                    during_disaster = table.Column<int>(type: "int", nullable: false),
                    not_known = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dur_dis_sufferings_according_to_cause_of_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_dur_dis_sufferings_according_to_cause_of_disease_lkp_admin_b~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dur_dis_sufferings_according_to_type_of_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    diarrhoea = table.Column<int>(type: "int", nullable: false),
                    dysentery = table.Column<int>(type: "int", nullable: false),
                    malaria = table.Column<int>(type: "int", nullable: false),
                    skin_disease = table.Column<int>(type: "int", nullable: false),
                    cold_or_cough = table.Column<int>(type: "int", nullable: false),
                    fever = table.Column<int>(type: "int", nullable: false),
                    typhoid = table.Column<int>(type: "int", nullable: false),
                    asthma = table.Column<int>(type: "int", nullable: false),
                    jaundice = table.Column<int>(type: "int", nullable: false),
                    malnutrition_related = table.Column<int>(type: "int", nullable: false),
                    dengue = table.Column<int>(type: "int", nullable: false),
                    chikungunia = table.Column<int>(type: "int", nullable: false),
                    mental_disorder = table.Column<int>(type: "int", nullable: false),
                    chicken_pox = table.Column<int>(type: "int", nullable: false),
                    cholera = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dur_dis_sufferings_according_to_type_of_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_dur_dis_sufferings_according_to_type_of_disease_lkp_admin_bo~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ill_due_dis_not_suffering_according_to_sex",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    total_population = table.Column<int>(type: "int", nullable: false),
                    male = table.Column<int>(type: "int", nullable: false),
                    female = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ill_due_dis_not_suffering_according_to_sex", x => x.id);
                    table.ForeignKey(
                        name: "FK_ill_due_dis_not_suffering_according_to_sex_lkp_admin_boundar~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ill_due_dis_suffering_according_to_sex_age",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    total_household = table.Column<int>(type: "int", nullable: false),
                    household_age_0_to_4 = table.Column<int>(type: "int", nullable: false),
                    household_age_5_to_17 = table.Column<int>(type: "int", nullable: false),
                    household_age_18_to_36 = table.Column<int>(type: "int", nullable: false),
                    household_age_37_to_60 = table.Column<int>(type: "int", nullable: false),
                    household_age_61_plus = table.Column<int>(type: "int", nullable: false),
                    total_male = table.Column<int>(type: "int", nullable: false),
                    male_age_0_to_4 = table.Column<int>(type: "int", nullable: false),
                    male_age_5_to_17 = table.Column<int>(type: "int", nullable: false),
                    male_age_18_to_36 = table.Column<int>(type: "int", nullable: false),
                    male_age_37_to_60 = table.Column<int>(type: "int", nullable: false),
                    male_age_61_plus = table.Column<int>(type: "int", nullable: false),
                    total_female = table.Column<int>(type: "int", nullable: false),
                    female_age_0_to_4 = table.Column<int>(type: "int", nullable: false),
                    female_age_5_to_17 = table.Column<int>(type: "int", nullable: false),
                    female_age_18_to_36 = table.Column<int>(type: "int", nullable: false),
                    female_age_37_to_60 = table.Column<int>(type: "int", nullable: false),
                    female_age_61_plus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ill_due_dis_suffering_according_to_sex_age", x => x.id);
                    table.ForeignKey(
                        name: "FK_ill_due_dis_suffering_according_to_sex_age_lkp_admin_boundar~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ill_due_dis_suffering_according_to_type_of_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    diarrhoea = table.Column<int>(type: "int", nullable: false),
                    dysentery = table.Column<int>(type: "int", nullable: false),
                    malaria = table.Column<int>(type: "int", nullable: false),
                    skin_disease = table.Column<int>(type: "int", nullable: false),
                    cold_or_cough = table.Column<int>(type: "int", nullable: false),
                    fever = table.Column<int>(type: "int", nullable: false),
                    typhoid = table.Column<int>(type: "int", nullable: false),
                    asthma = table.Column<int>(type: "int", nullable: false),
                    jaundice = table.Column<int>(type: "int", nullable: false),
                    malnutrition_related = table.Column<int>(type: "int", nullable: false),
                    dengue = table.Column<int>(type: "int", nullable: false),
                    chikungunia = table.Column<int>(type: "int", nullable: false),
                    mental_disorder = table.Column<int>(type: "int", nullable: false),
                    chicken_pox = table.Column<int>(type: "int", nullable: false),
                    cholera = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ill_due_dis_suffering_according_to_type_of_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_ill_due_dis_suffering_according_to_type_of_disease_lkp_admin~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pst_dis_sufferings_according_to_cause_of_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    temperature_variation = table.Column<int>(type: "int", nullable: false),
                    variation_in_rain = table.Column<int>(type: "int", nullable: false),
                    water_pollution = table.Column<int>(type: "int", nullable: false),
                    air_pollution = table.Column<int>(type: "int", nullable: false),
                    unplanned_sanitation = table.Column<int>(type: "int", nullable: false),
                    during_disaster = table.Column<int>(type: "int", nullable: false),
                    not_known = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pst_dis_sufferings_according_to_cause_of_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_pst_dis_sufferings_according_to_cause_of_disease_lkp_admin_b~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pst_dis_sufferings_according_to_type_of_disease",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    diarrhoea = table.Column<int>(type: "int", nullable: false),
                    dysentery = table.Column<int>(type: "int", nullable: false),
                    malaria = table.Column<int>(type: "int", nullable: false),
                    skin_disease = table.Column<int>(type: "int", nullable: false),
                    cold_or_cough = table.Column<int>(type: "int", nullable: false),
                    fever = table.Column<int>(type: "int", nullable: false),
                    typhoid = table.Column<int>(type: "int", nullable: false),
                    asthma = table.Column<int>(type: "int", nullable: false),
                    jaundice = table.Column<int>(type: "int", nullable: false),
                    malnutrition_related = table.Column<int>(type: "int", nullable: false),
                    dengue = table.Column<int>(type: "int", nullable: false),
                    chikungunia = table.Column<int>(type: "int", nullable: false),
                    mental_disorder = table.Column<int>(type: "int", nullable: false),
                    chicken_pox = table.Column<int>(type: "int", nullable: false),
                    cholera = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pst_dis_sufferings_according_to_type_of_disease", x => x.id);
                    table.ForeignKey(
                        name: "FK_pst_dis_sufferings_according_to_type_of_disease_lkp_admin_bo~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "respondent_perception_about_disaster_management",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    to_reduce_loss_in_a_systematic_manner = table.Column<int>(type: "int", nullable: false),
                    assist_only_affected_people = table.Column<int>(type: "int", nullable: false),
                    to_stand_beside_the_affected_people = table.Column<int>(type: "int", nullable: false),
                    do_not_know = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respondent_perception_about_disaster_management", x => x.id);
                    table.ForeignKey(
                        name: "FK_respondent_perception_about_disaster_management_lkp_admin_bo~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "respondent_perception_about_impact_climate_cng",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    sea_level_rise = table.Column<int>(type: "int", nullable: false),
                    drought_or_dryness = table.Column<int>(type: "int", nullable: false),
                    flood_or_water_logging = table.Column<int>(type: "int", nullable: false),
                    salinity = table.Column<int>(type: "int", nullable: false),
                    storm_or_tornado_or_hailstorm = table.Column<int>(type: "int", nullable: false),
                    tidal_surge_or_cyclone_or_hurricane = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respondent_perception_about_impact_climate_cng", x => x.id);
                    table.ForeignKey(
                        name: "FK_respondent_perception_about_impact_climate_cng_lkp_admin_bou~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "respondents_perception_about_climate_change",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    ltc_due_to_natural_or_human_act = table.Column<int>(type: "int", nullable: false),
                    reg_variation_in_temp_and_rainfall = table.Column<int>(type: "int", nullable: false),
                    ext_evnt_cus_loss_human_life_n_struc = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false),
                    do_not_know = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respondents_perception_about_climate_change", x => x.id);
                    table.ForeignKey(
                        name: "FK_respondents_perception_about_climate_change_lkp_admin_bounda~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "respondents_perception_about_disaster",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    critical_sis_by_nature_or_Hman = table.Column<int>(type: "int", nullable: false),
                    usual_process_occurs_time_to_time = table.Column<int>(type: "int", nullable: false),
                    happens_without_any_reason = table.Column<int>(type: "int", nullable: false),
                    do_not_know = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respondents_perception_about_disaster", x => x.id);
                    table.ForeignKey(
                        name: "FK_respondents_perception_about_disaster_lkp_admin_boundary_dis~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "trtment_faclti_rcv_by_hh_insufficient_wtr_sply",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dist_geo_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    do_not_do_anything = table.Column<int>(type: "int", nullable: false),
                    self_treatment = table.Column<int>(type: "int", nullable: false),
                    medicine_shop_or_pharmacy = table.Column<int>(type: "int", nullable: false),
                    kobiraj_or_ohja = table.Column<int>(type: "int", nullable: false),
                    mbbs_doctor = table.Column<int>(type: "int", nullable: false),
                    district_government_hospital = table.Column<int>(type: "int", nullable: false),
                    upazila_health_n_family_welfare_clinic = table.Column<int>(type: "int", nullable: false),
                    union_health_n_family_welfare_clinic = table.Column<int>(type: "int", nullable: false),
                    community_clinic = table.Column<int>(type: "int", nullable: false),
                    village_doctor = table.Column<int>(type: "int", nullable: false),
                    homeo_doctor = table.Column<int>(type: "int", nullable: false),
                    others = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trtment_faclti_rcv_by_hh_insufficient_wtr_sply", x => x.id);
                    table.ForeignKey(
                        name: "FK_trtment_faclti_rcv_by_hh_insufficient_wtr_sply_lkp_admin_bou~",
                        column: x => x.dist_geo_code,
                        principalTable: "lkp_admin_boundary_districts",
                        principalColumn: "dist_geo_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bfor_dis_sufferings_according_to_cause_of_disease_dist_geo_c~",
                table: "bfor_dis_sufferings_according_to_cause_of_disease",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_bfor_dis_sufferings_according_to_type_of_disease_dist_geo_co~",
                table: "bfor_dis_sufferings_according_to_type_of_disease",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_domestic_water_by_source_bfr_disaster_dist_geo_code",
                table: "domestic_water_by_source_bfr_disaster",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_domestic_water_by_source_dur_disaster_dist_geo_code",
                table: "domestic_water_by_source_dur_disaster",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_drinking_water_by_source_dur_disaster_dist_geo_code",
                table: "drinking_water_by_source_dur_disaster",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_dur_dis_sufferings_according_to_cause_of_disease_dist_geo_co~",
                table: "dur_dis_sufferings_according_to_cause_of_disease",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_dur_dis_sufferings_according_to_type_of_disease_dist_geo_code",
                table: "dur_dis_sufferings_according_to_type_of_disease",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_ill_due_dis_not_suffering_according_to_sex_dist_geo_code",
                table: "ill_due_dis_not_suffering_according_to_sex",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_ill_due_dis_suffering_according_to_sex_age_dist_geo_code",
                table: "ill_due_dis_suffering_according_to_sex_age",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_ill_due_dis_suffering_according_to_type_of_disease_dist_geo_~",
                table: "ill_due_dis_suffering_according_to_type_of_disease",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_pst_dis_sufferings_according_to_cause_of_disease_dist_geo_co~",
                table: "pst_dis_sufferings_according_to_cause_of_disease",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_pst_dis_sufferings_according_to_type_of_disease_dist_geo_code",
                table: "pst_dis_sufferings_according_to_type_of_disease",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_respondent_perception_about_disaster_management_dist_geo_code",
                table: "respondent_perception_about_disaster_management",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_respondent_perception_about_impact_climate_cng_dist_geo_code",
                table: "respondent_perception_about_impact_climate_cng",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_respondents_perception_about_climate_change_dist_geo_code",
                table: "respondents_perception_about_climate_change",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_respondents_perception_about_disaster_dist_geo_code",
                table: "respondents_perception_about_disaster",
                column: "dist_geo_code");

            migrationBuilder.CreateIndex(
                name: "IX_trtment_faclti_rcv_by_hh_insufficient_wtr_sply_dist_geo_code",
                table: "trtment_faclti_rcv_by_hh_insufficient_wtr_sply",
                column: "dist_geo_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bfor_dis_sufferings_according_to_cause_of_disease");

            migrationBuilder.DropTable(
                name: "bfor_dis_sufferings_according_to_type_of_disease");

            migrationBuilder.DropTable(
                name: "domestic_water_by_source_bfr_disaster");

            migrationBuilder.DropTable(
                name: "domestic_water_by_source_dur_disaster");

            migrationBuilder.DropTable(
                name: "drinking_water_by_source_dur_disaster");

            migrationBuilder.DropTable(
                name: "dur_dis_sufferings_according_to_cause_of_disease");

            migrationBuilder.DropTable(
                name: "dur_dis_sufferings_according_to_type_of_disease");

            migrationBuilder.DropTable(
                name: "ill_due_dis_not_suffering_according_to_sex");

            migrationBuilder.DropTable(
                name: "ill_due_dis_suffering_according_to_sex_age");

            migrationBuilder.DropTable(
                name: "ill_due_dis_suffering_according_to_type_of_disease");

            migrationBuilder.DropTable(
                name: "pst_dis_sufferings_according_to_cause_of_disease");

            migrationBuilder.DropTable(
                name: "pst_dis_sufferings_according_to_type_of_disease");

            migrationBuilder.DropTable(
                name: "respondent_perception_about_disaster_management");

            migrationBuilder.DropTable(
                name: "respondent_perception_about_impact_climate_cng");

            migrationBuilder.DropTable(
                name: "respondents_perception_about_climate_change");

            migrationBuilder.DropTable(
                name: "respondents_perception_about_disaster");

            migrationBuilder.DropTable(
                name: "trtment_faclti_rcv_by_hh_insufficient_wtr_sply");
        }
    }
}
