

namespace Web.Api.Models.Request
{
  public class CadastrarUsuarioRequest
  {
    public string Nome { get; set; }
    public string Senha { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public string Cpf { get; set; }
  }
}
