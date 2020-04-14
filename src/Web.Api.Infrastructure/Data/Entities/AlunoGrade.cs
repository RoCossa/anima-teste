using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Infrastructure.Data.Entities
{
    public class AlunoGrade
    {
        [Key]
        public int IdAlunoGrade { get; set; }
        public int IdAluno { get; set; }
        [ForeignKey("Grade")]
        public int IdGrade { get; set; }
        public Grade Grade { get; set; }
    }
}
