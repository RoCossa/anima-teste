using System.ComponentModel.DataAnnotations;

namespace Web.Api.Infrastructure.Data.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Cpf { get; set; }
    }
}
