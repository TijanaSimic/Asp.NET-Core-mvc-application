using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Project.Models.Database 
{
    public class ProfessorSubject {
        
        public int idProf {get; set;}
 
        public Professor professor {get;set;}

        public int idSubj {get;set;}
     
        public Subject subject {get;set;}

    }
    public class ProfessorSubjectConfiguration : IEntityTypeConfiguration<ProfessorSubject> {
        public void Configure(EntityTypeBuilder<ProfessorSubject> builder) {
            builder.HasKey (
                entity => new { entity.idProf, entity.idSubj }
            );

         
        }
    }
}