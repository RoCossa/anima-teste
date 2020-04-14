using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.UseCases;
using Web.Api.Presenters;
using Xunit;

namespace Web.Api.UnitTests.Controllers
{
    public class AlunoControllerUnitTests
    {
        [Fact]
        public async void Post_Retorna_Ok_Em_Caso_De_Sucesso_No_Use_Case()
        {
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

            var outputPort = new CadastrarAlunoPresenter();
            var useCase = new CadastrarAlunoUseCase(mockUsuarioRepository.Object);
            
            var controller = new AlunoController(useCase, outputPort); 
            var result = await controller.Post(new Models.Request.CadastrarAlunoRequest());

            var statusCode = ((ContentResult) result).StatusCode;
            Assert.True(statusCode.HasValue && statusCode.Value == (int) HttpStatusCode.OK);
        }
    }
}
