using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

 namespace Project.Models.Database {

     public class Subject {

         [Key]
         public int idSub {get;set;}
         [Required]
         [Display(Name = "Subject Name")]
         public string name {get;set;}

         public ICollection<Exam> exams {get;set;}
        [Display(Name = "Professors")]
        public ICollection<ProfessorSubject> professorSubjectList {get;set;}
   
        //lista koja ce uhvatiti sve id-eve iz multiliste
        [Required]
        [NotMapped] 
        [Display (Name = "Professors")]
        public IEnumerable<int> professors {get;set;}

        [Display(Name = "Status")]
        [Required]
        public string activity  {get;set;}

        public bool isActive() {
           
           if(activity == "active") return true;
           return false;
        }


          

     }

     public class SubjectConfiguration:IEntityTypeConfiguration<Subject> {

     public void Configure(EntityTypeBuilder<Subject> builder) {
        builder.Property(subject => subject.idSub)
        .ValueGeneratedOnAdd();

       
       
     }

 }
 }