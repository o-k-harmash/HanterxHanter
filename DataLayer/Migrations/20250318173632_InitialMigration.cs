using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    InterestId = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.InterestId);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipGoals",
                columns: table => new
                {
                    RelationshipGoalId = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipGoals", x => x.RelationshipGoalId);
                });

            migrationBuilder.CreateTable(
                name: "SexualOrientations",
                columns: table => new
                {
                    SexualOrientationId = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SexualOrientations", x => x.SexualOrientationId);
                });

            migrationBuilder.CreateTable(
                name: "Translates",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    ToTable = table.Column<string>(type: "text", nullable: false),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translates", x => new { x.ToTable, x.Key, x.LangCode });
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CountryId = table.Column<string>(type: "character varying(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<byte[]>(type: "bytea", nullable: false),
                    GenderId = table.Column<string>(type: "character varying(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenderId = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<string>(type: "text", nullable: false),
                    SexualOrientationId = table.Column<string>(type: "character varying(40)", nullable: false),
                    RelationshipGoalId = table.Column<string>(type: "character varying(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Profiles_RelationshipGoals_RelationshipGoalId",
                        column: x => x.RelationshipGoalId,
                        principalTable: "RelationshipGoals",
                        principalColumn: "RelationshipGoalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profiles_SexualOrientations_SexualOrientationId",
                        column: x => x.SexualOrientationId,
                        principalTable: "SexualOrientations",
                        principalColumn: "SexualOrientationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    StateId = table.Column<string>(type: "character varying(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<string>(type: "text", nullable: false),
                    CityId = table.Column<string>(type: "text", nullable: false),
                    StateId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Address_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    FileId = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    ProfileUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_File_Profiles_ProfileUserId",
                        column: x => x.ProfileUserId,
                        principalTable: "Profiles",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ProfileInterest",
                columns: table => new
                {
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    InterestId = table.Column<string>(type: "character varying(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileInterest", x => new { x.ProfileId, x.InterestId });
                    table.ForeignKey(
                        name: "FK_ProfileInterest_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileInterest_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileLanguage",
                columns: table => new
                {
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageId = table.Column<string>(type: "character varying(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileLanguage", x => new { x.ProfileId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ProfileLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileLanguage_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_File_ProfileUserId",
                table: "File",
                column: "ProfileUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileInterest_InterestId",
                table: "ProfileInterest",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLanguage_LanguageId",
                table: "ProfileLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_GenderId_CityId_Age",
                table: "Profiles",
                columns: new[] { "GenderId", "CityId", "Age" });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_RelationshipGoalId",
                table: "Profiles",
                column: "RelationshipGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_SexualOrientationId",
                table: "Profiles",
                column: "SexualOrientationId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "ProfileInterest");

            migrationBuilder.DropTable(
                name: "ProfileLanguage");

            migrationBuilder.DropTable(
                name: "Translates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "RelationshipGoals");

            migrationBuilder.DropTable(
                name: "SexualOrientations");
        }
    }
}
