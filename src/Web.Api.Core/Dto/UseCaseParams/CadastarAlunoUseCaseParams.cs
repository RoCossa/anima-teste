using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseParams
{
    public class CadastrarAlunoUseCaseParams : IUseCaseRequest<GenericDataUseCaseResponse<Aluno>>
    {
        public string Nome { get; }
        public string Senha { get; }
        public string Email { get; }
        public string Login { get; }
        public string Cpf { get; }
        public int Ra { get; }

        public CadastrarAlunoUseCaseParams(string nome, string senha, string email, string login, string cpf, int ra)
        {
            Nome = nome;
            Senha = senha;
            Email = email;
            Login = login;
            Cpf = cpf;
            Ra = ra;
        }
    }
}
