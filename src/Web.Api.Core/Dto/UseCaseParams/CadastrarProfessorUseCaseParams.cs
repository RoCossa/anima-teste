using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseParams
{
    public class CadastrarProfessorUseCaseParams : IUseCaseRequest<GenericDataUseCaseResponse<Professor>>
    {
        public string Nome { get; }
        public string Senha { get; }
        public string Email { get; }
        public string Login { get; }
        public string Cpf { get; }
        public int Codigo { get; }

        public CadastrarProfessorUseCaseParams(string nome, string senha, string email, string login, string cpf, int codigo)
        {
            Nome = nome;
            Senha = senha;
            Email = email;
            Login = login;
            Cpf = cpf;
            Codigo = codigo;
        }
    }
}
