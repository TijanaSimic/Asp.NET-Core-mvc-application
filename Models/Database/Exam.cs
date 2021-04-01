using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
namespace Project.Models.Database { 

public class Exam {

    [Key]
    public int idExam {get;set;} 
    [Required] 
    [Display(Name = "Date")]
    public DateTime date {get; set;}
    [Required]
     [Display(Name = "Subject")]
    public int idSubj {get; set;} 
    public Subject subject {get;set;}
    [Display(Name = "Subject")]
    public string subjName {get;set;}

    public ICollection<StudentExam> studentExamList {get;set;}

  public string nameDate =>subjName + " - "+ this.date.ToString();
 


}

public class ExamConfiguration :IEntityTypeConfiguration<Exam> {
    public void Configure(EntityTypeBuilder<Exam> builder) {
        builder.Property(exam => exam.idExam)
        .ValueGeneratedOnAdd();
    
        
    }
}
}