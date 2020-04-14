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
    public sealed class CadastrarProfessorUseCase : ICadastrarProfessorUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CadastrarProfessorUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(CadastrarProfessorUseCaseParams message, IOutputPort<GenericDataUseCaseResponse<Professor>> outputPort)
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
                var responseProfessor = await _usuarioRepository.CriarProfessor(new Domain.Entities.Professor()
                {
                    Codigo = message.Codigo,
                    IdUsuario = response.Data.IdUsuario,
                });
                outputPort.Handle(responseProfessor.Success ? new GenericDataUseCaseResponse<Professor>(responseProfessor.Data, true) : new GenericDataUseCaseResponse<Professor>(response.Errors.Select(e => e.Description)));
                return responseProfessor.Success;
            }
            else
            {
                outputPort.Handle(new GenericDataUseCaseResponse<Professor>(response.Errors.Select(e => e.Description)));
                return response.Success;
            }            
        }
    }
}
