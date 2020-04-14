using System.Threading.Tasks;
using Moq;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.UseCases;
using Xunit;

namespace Web.Api.Core.UnitTests.UseCases
{
    public class CadastrarAlunoUseCaseUnitTests
    {

        [Fact]
        public async void Cadastar_Aluno_Com_Sucesso()
        {
            // Mock dos repository
            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            mockUsuarioRepository
              .Setup(r => r.CriarUsuario(It.IsAny<Usuario>()))
              .Returns(Task.FromResult(new DbGenericResponse<Usuario>(new Usuario()
              {
                  Cpf = "12345678962",
                  Email = "teste@email.com",
                  IdUsuario = 1,
                  Login = "teste",
                  Nome = "Teste",
                  Senha = "Teste123",
              }, true)));

            mockUsuarioRepository
              .Setup(r => r.CriarAluno(It.IsAny<Aluno>()))
              .Returns(Task.FromResult(new DbGenericResponse<Aluno>(new Aluno()
              {
                  IdAluno = 1,
                  IdUsuario = 1,
                  Ra = 123,
                  Usuario = new Usuario()
                  {
                      Cpf = "12345678962",
                      Email = "teste@email.com",
                      IdUsuario = 1,
                      Login = "teste",
                      Nome = "Teste",
                      Senha = "Teste123",
                  }
              }, true)));

            var useCase = new CadastrarAlunoUseCase(mockUsuarioRepository.Object);

            var mockOutputPort = new Mock<IOutputPort<GenericDataUseCaseResponse<Aluno>>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<GenericDataUseCaseResponse<Aluno>>()));

            var response = await useCase.Handle(new Dto.UseCaseParams.CadastrarAlunoUseCaseParams("Teste", "teste123", "teste@email.com", "teste", "12345678962", 123), mockOutputPort.Object);

            Assert.True(response);
        }
    }
}
