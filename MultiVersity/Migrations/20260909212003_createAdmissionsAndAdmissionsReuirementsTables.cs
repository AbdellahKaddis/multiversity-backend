using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiVersity.Migrations
{
    /// <inheritdoc />
    public partial class createAdmissionsAndAdmissionsReuirementsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admission",
                columns: table => new
                {
                    AdmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Process = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admission", x => x.AdmissionId);
                    table.ForeignKey(
                        name: "FK_Admission_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionRequirement",
                columns: table => new
                {
                    AdmissionRequirementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionRequirement", x => x.AdmissionRequirementId);
                    table.ForeignKey(
                        name: "FK_AdmissionRequirement_Admission_AdmissionId",
                        column: x => x.AdmissionId,
                        principalTable: "Admission",
                        principalColumn: "AdmissionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admission_ProgramId",
                table: "Admission",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionRequirement_AdmissionId",
                table: "AdmissionRequirement",
                column: "AdmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdmissionRequirement");

            migrationBuilder.DropTable(
                name: "Admission");
        }
    }
}
