using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiVersity.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFacultyDeanRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_AspNetUsers_DeanId",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_DeanId",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "DeanId",
                table: "Faculties");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FacultyDeans",
                columns: table => new
                {
                    FacultyDeanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyDeans", x => x.FacultyDeanId);
                    table.ForeignKey(
                        name: "FK_FacultyDeans_AspNetUsers_DeanId",
                        column: x => x.DeanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacultyDeans_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyDeans_DeanId",
                table: "FacultyDeans",
                column: "DeanId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyDeans_FacultyId",
                table: "FacultyDeans",
                column: "FacultyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyDeans");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "DeanId",
                table: "Faculties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_DeanId",
                table: "Faculties",
                column: "DeanId",
                unique: true,
                filter: "[DeanId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_AspNetUsers_DeanId",
                table: "Faculties",
                column: "DeanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
