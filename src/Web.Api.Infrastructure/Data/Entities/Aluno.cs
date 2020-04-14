using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Infrastructure.Data.Entities
{
    public class Aluno
    {
        [Key]
        public int IdAluno { get; set; }
        
        public int Ra { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
