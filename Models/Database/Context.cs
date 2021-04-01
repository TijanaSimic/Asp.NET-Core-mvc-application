using Microsoft.EntityFrameworkCore;
using Project.Models.Database;

namespace Project.Models.Database {

    public class ProjectContext : DbContext {

        public ProjectContext(DbContextOptions options) : base(options) {}
            public DbSet<Professor> professors {get;set;}
            public DbSet<Subject> subjects {get;set;}
            public DbSet<ProfessorSubject> professorSubjects {get;set;}
            public DbSet<Student> students {get;set;}
            public DbSet<Exam> exams {get;set;}
            public DbSet<StudentExam> studentExams {get;set;}
            protected override void OnModelCreating(ModelBuilder builder) {
                base.OnModelCreating(builder);

                builder.ApplyConfiguration(new ProfessorConfiguration());
                builder.ApplyConfiguration(new SubjectConfiguration());
                builder.ApplyConfiguration(new StudentExamConfiguration());
                builder.ApplyConfiguration(new StudentConfiguration());
                builder.ApplyConfiguration(new ExamConfiguration());
                builder.ApplyConfiguration(new ProfessorSubjectConfiguration());
            }

        }

    }
 