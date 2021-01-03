using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class course4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SemesterRegId",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SemesterRegId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SemesterRegs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    SemesterNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterRegs", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_SemesterRegs_SemesterRegId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_SemesterRegs_SemesterRegId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "SemesterRegs");

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
        }
    }
}
