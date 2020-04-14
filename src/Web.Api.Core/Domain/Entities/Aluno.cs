
namespace Web.Api.Core.Domain.Entities
{
    public class Aluno
    {
        public int IdAluno { get; set; }
        public int Ra { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
