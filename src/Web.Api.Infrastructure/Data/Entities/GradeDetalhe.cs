using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Infrastructure.Data.Entities
{
    public class GradeDetalhe
    {
        [Key]
        public int IdGradeDetalhe { get; set; }
        
        public string Curso { get; set; }
        public string Disciplina { get; set; }
        public string Turma { get; set; }
        public int CodGrade { get; set; }
        [ForeignKey("Professor")]
        public int IdProfessor { get; set; }
        public Professor Professor { get; set; }
    }
}
