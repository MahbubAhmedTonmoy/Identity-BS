using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class course3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Students_StudentID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeachersId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StudentID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeachersId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeachersId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CoursesId",
                table: "Teachers",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CoursesId",
                table: "Students",
                column: "CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CoursesId",
                table: "Students",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Courses_CoursesId",
                table: "Teachers",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CoursesId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Courses_CoursesId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_CoursesId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_CoursesId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeachersId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: true),
                    TeachersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudentID",
                table: "Courses",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeachersId",
                table: "Courses",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_StudentsId",
                table: "Course",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_TeachersId",
                table: "Course",
                column: "TeachersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Students_StudentID",
                table: "Courses",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeachersId",
                table: "Courses",
                column: "TeachersId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
