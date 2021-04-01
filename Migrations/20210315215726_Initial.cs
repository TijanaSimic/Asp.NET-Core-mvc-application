using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "professors",
                columns: table => new
                {
                    idProf = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professors", x => x.idProf);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    idStud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.idStud);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    idSub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    activity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.idSub);
                });

            migrationBuilder.CreateTable(
                name: "exams",
                columns: table => new
                {
                    idExam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idSubj = table.Column<int>(type: "int", nullable: false),
                    subjectidSub = table.Column<int>(type: "int", nullable: true),
                    subjName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exams", x => x.idExam);
                    table.ForeignKey(
                        name: "FK_exams_subjects_subjectidSub",
                        column: x => x.subjectidSub,
                        principalTable: "subjects",
                        principalColumn: "idSub",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "professorSubjects",
                columns: table => new
                {
                    idProf = table.Column<int>(type: "int", nullable: false),
                    idSubj = table.Column<int>(type: "int", nullable: false),
                    professoridProf = table.Column<int>(type: "int", nullable: true),
                    subjectidSub = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professorSubjects", x => new { x.idProf, x.idSubj });
                    table.ForeignKey(
                        name: "FK_professorSubjects_professors_professoridProf",
                        column: x => x.professoridProf,
                        principalTable: "professors",
                        principalColumn: "idProf",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_professorSubjects_subjects_subjectidSub",
                        column: x => x.subjectidSub,
                        principalTable: "subjects",
                        principalColumn: "idSub",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "studentExams",
                columns: table => new
                {
                    idStud = table.Column<int>(type: "int", nullable: false),
                    idExam = table.Column<int>(type: "int", nullable: false),
                    studentidStud = table.Column<int>(type: "int", nullable: true),
                    examidExam = table.Column<int>(type: "int", nullable: true),
                    mark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentExams", x => new { x.idStud, x.idExam });
                    table.ForeignKey(
                        name: "FK_studentExams_exams_examidExam",
                        column: x => x.examidExam,
                        principalTable: "exams",
                        principalColumn: "idExam",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_studentExams_students_studentidStud",
                        column: x => x.studentidStud,
                        principalTable: "students",
                        principalColumn: "idStud",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "professors",
                columns: new[] { "idProf", "firstName", "lastName" },
                values: new object[,]
                {
                    { 1, "Milos", "Milosevic" },
                    { 2, "Mina", "Milicc" },
                    { 3, "Milica", "Mirkovic" }
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "idStud", "firstName", "lastName" },
                values: new object[] { 1, "Marko", "Markovic" });

            migrationBuilder.CreateIndex(
                name: "IX_exams_subjectidSub",
                table: "exams",
                column: "subjectidSub");

            migrationBuilder.CreateIndex(
                name: "IX_professorSubjects_professoridProf",
                table: "professorSubjects",
                column: "professoridProf");

            migrationBuilder.CreateIndex(
                name: "IX_professorSubjects_subjectidSub",
                table: "professorSubjects",
                column: "subjectidSub");

            migrationBuilder.CreateIndex(
                name: "IX_studentExams_examidExam",
                table: "studentExams",
                column: "examidExam");

            migrationBuilder.CreateIndex(
                name: "IX_studentExams_studentidStud",
                table: "studentExams",
                column: "studentidStud");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "professorSubjects");

            migrationBuilder.DropTable(
                name: "studentExams");

            migrationBuilder.DropTable(
                name: "professors");

            migrationBuilder.DropTable(
                name: "exams");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "subjects");
        }
    }
}
