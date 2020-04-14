using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Infrastructure.Data.Entities
{
    public class Grade
    {
        [Key]
        public int IdGrade { get; set; }
        [ForeignKey("GradeDetalhe")]
        public int IdGradeDetalhe { get; set; }
        public GradeDetalhe GradeDetalhe { get; set; }
    }
}
