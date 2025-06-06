using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Missing_SchoolYear_Relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolYearId",
                table: "Timetables",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolYearId",
                table: "Grades",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolYearId",
                table: "Absences",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Timetables_SchoolYearId",
                table: "Timetables",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_SchoolYearId",
                table: "Grades",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_SchoolYearId",
                table: "Absences",
                column: "SchoolYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_SchoolYears_SchoolYearId",
                table: "Absences",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_SchoolYears_SchoolYearId",
                table: "Grades",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Timetables_SchoolYears_SchoolYearId",
                table: "Timetables",
                column: "SchoolYearId",
                principalTable: "SchoolYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_SchoolYears_SchoolYearId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_SchoolYears_SchoolYearId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Timetables_SchoolYears_SchoolYearId",
                table: "Timetables");

            migrationBuilder.DropIndex(
                name: "IX_Timetables_SchoolYearId",
                table: "Timetables");

            migrationBuilder.DropIndex(
                name: "IX_Grades_SchoolYearId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Absences_SchoolYearId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "Timetables");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "Absences");
        }
    }
}
