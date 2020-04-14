

namespace Web.Api.Models.Request
{
    public class CadastrarAlunoRequest
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Cpf { get; set; }
        public int Ra { get; set; }
    }
}
