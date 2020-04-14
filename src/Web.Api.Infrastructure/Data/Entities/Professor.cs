using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Infrastructure.Data.Entities
{
    public class Professor
    {
        [Key]
        public int IdProfessor { get; set; }
        
        public int Codigo { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
