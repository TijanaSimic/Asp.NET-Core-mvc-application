using System.ComponentModel.DataAnnotations;
using System;

namespace Project.Models
{
    public class ProfessorExams
    {
       [Display(Name = "From")]
        public DateTime date1 {get;set;}
       [Display(Name = "To")]
        public DateTime date2 {get;set;}     
       [Display(Name = "Professor")]
        public int idProf {get; set;} 
    }
}