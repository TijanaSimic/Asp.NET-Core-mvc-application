using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
namespace Project.Models.Database {

    public class Student {
        [Key]
        [Display(Name = "Index")]
        public int idStud {get; set;}
        [Display(Name = "First Name")]
        [Required]
        public string firstName {get;set;}
        [Required]
        [Display(Name = "Last Name")]
        public string lastName {get; set;}

        public string fullName => this.firstName + " " + this.lastName;

    [Display(Name = "Exams")]
     public ICollection<StudentExam> studentExamList { get; set; }
       
    }

    public class StudentConfiguration:IEntityTypeConfiguration<Student> {
        public void Configure(EntityTypeBuilder<Student> builder) {
            builder.Property(student => student.idStud) 
            .ValueGeneratedOnAdd();

            builder.HasData(
                new Student () {
                    idStud=1,
                    firstName = "Marko",
                    lastName = "Markovic"
                }
            );
        }
    }


}