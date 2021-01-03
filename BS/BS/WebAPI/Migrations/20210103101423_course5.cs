using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class course5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_SemesterRegs_SemesterRegId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_SemesterRegs_SemesterRegId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SemesterRegId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_SemesterRegId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SemesterRegId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SemesterRegId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentsId",
                table: "SemesterRegs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeachersId",
                table: "SemesterRegs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRegs_StudentsId",
                table: "SemesterRegs",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterRegs_TeachersId",
                table: "SemesterRegs",
                column: "TeachersId");

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterRegs_Students_StudentsId",
                table: "SemesterRegs",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterRegs_Teachers_TeachersId",
                table: "SemesterRegs",
                column: "TeachersId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SemesterRegs_Students_StudentsId",
                table: "SemesterRegs");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterRegs_Teachers_TeachersId",
                table: "SemesterRegs");

            migrationBuilder.DropIndex(
                name: "IX_SemesterRegs_StudentsId",
                table: "SemesterRegs");

            migrationBuilder.DropIndex(
                name: "IX_SemesterRegs_TeachersId",
                table: "SemesterRegs");

            migrationBuilder.DropColumn(
                name: "StudentsId",
                table: "SemesterRegs");

            migrationBuilder.DropColumn(
                name: "TeachersId",
                table: "SemesterRegs");

            migrationBuilder.AddColumn<int>(
                name: "SemesterRegId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SemesterRegId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SemesterRegId",
                table: "Teachers",
                column: "SemesterRegId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SemesterRegId",
                table: "Students",
                column: "SemesterRegId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SemesterRegs_SemesterRegId",
                table: "Students",
                column: "SemesterRegId",
                principalTable: "SemesterRegs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_SemesterRegs_SemesterRegId",
                table: "Teachers",
                column: "SemesterRegId",
                principalTable: "SemesterRegs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
