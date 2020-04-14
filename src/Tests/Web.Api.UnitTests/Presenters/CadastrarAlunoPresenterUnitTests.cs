using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Presenters;
using Xunit;

namespace Web.Api.UnitTests.Presenters
{
    public class CadastrarAlunoPresenterUnitTests
    {
        [Fact]
        public void Status_OK_Em_Caso_De_Sucesso()
        {
            var presenter = new CadastrarAlunoPresenter();
            presenter.Handle(new GenericDataUseCaseResponse<Web.Api.Core.Domain.Entities.Aluno>(new Web.Api.Core.Domain.Entities.Aluno(), true));
            Assert.Equal((int)HttpStatusCode.OK, presenter.ContentResult.StatusCode);
        }

        [Fact]
        public void Contem_Erros_Em_Caso_De_Falha()
        {
            var presenter = new CadastrarAlunoPresenter();
            List<Error> errors = new List<Error>();
            Error error = new Error("500", "Erro qualquer");
            errors.Add(error);
            presenter.Handle(new GenericDataUseCaseResponse<Web.Api.Core.Domain.Entities.Aluno>(
                errors.Select(e => e.Description), false));

            dynamic data = JsonConvert.DeserializeObject(presenter.ContentResult.Content);
            Assert.False(data.success.Value);
            Assert.NotEmpty(data.errors);
        }

        [Fact]
        public void Contem_IdAluno_Em_Caso_De_Sucesso()
        {
            var presenter = new CadastrarAlunoPresenter();
            presenter.Handle(new GenericDataUseCaseResponse<Web.Api.Core.Domain.Entities.Aluno>(new Web.Api.Core.Domain.Entities.Aluno()
            {
                IdAluno = 1
            }, true));

            dynamic data = JsonConvert.DeserializeObject(presenter.ContentResult.Content);
            Assert.True(data["success"].Value);
            Assert.Equal(1, data["data"]["idAluno"].Value);
        }
    }
}
