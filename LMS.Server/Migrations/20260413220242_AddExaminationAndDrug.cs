using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddExaminationAndDrug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    DrugID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.DrugID);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    ExaminationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalID = table.Column<int>(type: "int", nullable: false),
                    VeterinarianID = table.Column<int>(type: "int", nullable: false),
                    ExaminationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Treatment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.ExaminationID);
                    table.ForeignKey(
                        name: "FK_Examinations_Animal_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animal",
                        principalColumn: "AnimalID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_Veterinarians_VeterinarianID",
                        column: x => x.VeterinarianID,
                        principalTable: "Veterinarians",
                        principalColumn: "VeterinarianID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationDrugs",
                columns: table => new
                {
                    ExaminationID = table.Column<int>(type: "int", nullable: false),
                    DrugID = table.Column<int>(type: "int", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDrugs", x => new { x.ExaminationID, x.DrugID });
                    table.ForeignKey(
                        name: "FK_ExaminationDrugs_Drugs_DrugID",
                        column: x => x.DrugID,
                        principalTable: "Drugs",
                        principalColumn: "DrugID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminationDrugs_Examinations_ExaminationID",
                        column: x => x.ExaminationID,
                        principalTable: "Examinations",
                        principalColumn: "ExaminationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDrugs_DrugID",
                table: "ExaminationDrugs",
                column: "DrugID");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_AnimalID",
                table: "Examinations",
                column: "AnimalID");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_VeterinarianID",
                table: "Examinations",
                column: "VeterinarianID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationDrugs");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "Examinations");
        }
    }
}
