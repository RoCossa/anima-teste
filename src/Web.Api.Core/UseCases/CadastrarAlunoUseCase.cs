using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseParams;

using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.Services;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class CadastrarAlunoUseCase : ICadastrarAlunoUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CadastrarAlunoUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(CadastrarAlunoUseCaseParams message, IOutputPort<GenericDataUseCaseResponse<Aluno>> outputPort)
        {
            var response = await _usuarioRepository.CriarUsuario(new Domain.Entities.Usuario()
            {
                Nome = message.Nome,
                Senha = message.Senha,
                Login = message.Login,
                Cpf = message.Cpf,
                Email = message.Email
            });
            if (response.Success)
            {
                var responseAluno = await _usuarioRepository.CriarAluno(new Domain.Entities.Aluno()
                {
                    Ra = message.Ra,
                    IdUsuario = response.Data.IdUsuario,
                });
                outputPort.Handle(responseAluno.Success ? new GenericDataUseCaseResponse<Aluno>(responseAluno.Data, true) : new GenericDataUseCaseResponse<Aluno>(response.Errors.Select(e => e.Description)));
                return responseAluno.Success;
            }
            else
            {
                outputPort.Handle(new GenericDataUseCaseResponse<Aluno>(response.Errors.Select(e => e.Description)));
                return response.Success;
            }            
        }
    }
}
