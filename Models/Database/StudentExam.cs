using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using Project.Models.Database;
using System.ComponentModel.DataAnnotations.Schema;


 namespace Project.Models.Database {

     public class StudentExam {

        [Display(Name = "Index")]
         [Required]
         public int idStud {get; set;}
         
         public Student student {get;set;}
         [Required]
          [Display(Name = "Exam")]
         public int idExam {get;set;}
   
         public Exam exam {get;set;} 
           [Display(Name = "Mark")]
          public int mark {get;set;}
     
 

     }

        public class StudentExamConfiguration : IEntityTypeConfiguration<StudentExam> {
        public void Configure(EntityTypeBuilder<StudentExam> builder) {
            builder.HasKey (
                entity => new { entity.idStud, entity.idExam }
            );

            
        }
    }
}