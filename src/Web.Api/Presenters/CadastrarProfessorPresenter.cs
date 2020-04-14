using System.Net;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Serialization;

namespace Web.Api.Presenters
{
    public sealed class CadastrarProfessorPresenter : IOutputPort<GenericDataUseCaseResponse<Professor>>
    {
        public JsonContentResult ContentResult { get; }

        public CadastrarProfessorPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GenericDataUseCaseResponse<Professor> response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
