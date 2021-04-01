﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Models.Database;

namespace Project.Migrations
{
    [DbContext(typeof(ProjectContext))]
    partial class ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project.Models.Database.Exam", b =>
                {
                    b.Property<int>("idExam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<int>("idSubj")
                        .HasColumnType("int");

                    b.Property<string>("subjName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("subjectidSub")
                        .HasColumnType("int");

                    b.HasKey("idExam");

                    b.HasIndex("subjectidSub");

                    b.ToTable("exams");
                });

            modelBuilder.Entity("Project.Models.Database.Professor", b =>
                {
                    b.Property<int>("idProf")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idProf");

                    b.ToTable("professors");

                    b.HasData(
                        new
                        {
                            idProf = 1,
                            firstName = "Milos",
                            lastName = "Milosevic"
                        },
                        new
                        {
                            idProf = 2,
                            firstName = "Mina",
                            lastName = "Milicc"
                        },
                        new
                        {
                            idProf = 3,
                            firstName = "Milica",
                            lastName = "Mirkovic"
                        });
                });

            modelBuilder.Entity("Project.Models.Database.ProfessorSubject", b =>
                {
                    b.Property<int>("idProf")
                        .HasColumnType("int");

                    b.Property<int>("idSubj")
                        .HasColumnType("int");

                    b.Property<int?>("professoridProf")
                        .HasColumnType("int");

                    b.Property<int?>("subjectidSub")
                        .HasColumnType("int");

                    b.HasKey("idProf", "idSubj");

                    b.HasIndex("professoridProf");

                    b.HasIndex("subjectidSub");

                    b.ToTable("professorSubjects");
                });

            modelBuilder.Entity("Project.Models.Database.Student", b =>
                {
                    b.Property<int>("idStud")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idStud");

                    b.ToTable("students");

                    b.HasData(
                        new
                        {
                            idStud = 1,
                            firstName = "Marko",
                            lastName = "Markovic"
                        });
                });

            modelBuilder.Entity("Project.Models.Database.StudentExam", b =>
                {
                    b.Property<int>("idStud")
                        .HasColumnType("int");

                    b.Property<int>("idExam")
                        .HasColumnType("int");

                    b.Property<int?>("examidExam")
                        .HasColumnType("int");

                    b.Property<int>("mark")
                        .HasColumnType("int");

                    b.Property<int?>("studentidStud")
                        .HasColumnType("int");

                    b.HasKey("idStud", "idExam");

                    b.HasIndex("examidExam");

                    b.HasIndex("studentidStud");

                    b.ToTable("studentExams");
                });

            modelBuilder.Entity("Project.Models.Database.Subject", b =>
                {
                    b.Property<int>("idSub")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("activity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idSub");

                    b.ToTable("subjects");
                });

            modelBuilder.Entity("Project.Models.Database.Exam", b =>
                {
                    b.HasOne("Project.Models.Database.Subject", "subject")
                        .WithMany("exams")
                        .HasForeignKey("subjectidSub");

                    b.Navigation("subject");
                });

            modelBuilder.Entity("Project.Models.Database.ProfessorSubject", b =>
                {
                    b.HasOne("Project.Models.Database.Professor", "professor")
                        .WithMany("professorSubjectList")
                        .HasForeignKey("professoridProf");

                    b.HasOne("Project.Models.Database.Subject", "subject")
                        .WithMany("professorSubjectList")
                        .HasForeignKey("subjectidSub");

                    b.Navigation("professor");

                    b.Navigation("subject");
                });

            modelBuilder.Entity("Project.Models.Database.StudentExam", b =>
                {
                    b.HasOne("Project.Models.Database.Exam", "exam")
                        .WithMany("studentExamList")
                        .HasForeignKey("examidExam");

                    b.HasOne("Project.Models.Database.Student", "student")
                        .WithMany("studentExamList")
                        .HasForeignKey("studentidStud");

                    b.Navigation("exam");

                    b.Navigation("student");
                });

            modelBuilder.Entity("Project.Models.Database.Exam", b =>
                {
                    b.Navigation("studentExamList");
                });

            modelBuilder.Entity("Project.Models.Database.Professor", b =>
                {
                    b.Navigation("professorSubjectList");
                });

            modelBuilder.Entity("Project.Models.Database.Student", b =>
                {
                    b.Navigation("studentExamList");
                });

            modelBuilder.Entity("Project.Models.Database.Subject", b =>
                {
                    b.Navigation("exams");

                    b.Navigation("professorSubjectList");
                });
#pragma warning restore 612, 618
        }
    }
}
