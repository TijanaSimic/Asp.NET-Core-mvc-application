using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 
namespace Project.Models.Database {

public class Professor {

    [Key]
    public int idProf {get;set;}
    [Required]
    [Display(Name = "Ime")]
    public string firstName {get; set;}
    [Display(Name = "Prezime")]
    [Required]
    public string lastName {get;set;}

    public string fullName => this.firstName + " " + this.lastName;

     public ICollection<ProfessorSubject> professorSubjectList {get;set;}

}
    public class ProfessorConfiguration:IEntityTypeConfiguration<Professor> {
        public void Configure(EntityTypeBuilder<Professor> builder) {

            builder.Property(professor => professor.idProf)
            .ValueGeneratedOnAdd();

            builder.HasData(
                new Professor() {
                    idProf = 1,
                    firstName = "Milos",
                    lastName = "Milosevic"
                }
            );
               builder.HasData(
                new Professor() {
                    idProf = 2,
                    firstName = "Mina",
                    lastName = "Milic"
                }
            );
               builder.HasData(
                new Professor() {
                    idProf = 3,
                    firstName = "Milica",
                    lastName = "Mirkovic"
                }
            );

        }
    }

}