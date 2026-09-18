using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiVersity.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "StudentNumberSeq");

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearLevel = table.Column<int>(type: "int", nullable: false),
                    EnrolledAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enrollments_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ApplicantId",
                table: "Enrollments",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_FacultyId",
                table: "Enrollments",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ProgramId",
                table: "Enrollments",
                column: "ProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropSequence(
                name: "StudentNumberSeq");
        }
    }
}
