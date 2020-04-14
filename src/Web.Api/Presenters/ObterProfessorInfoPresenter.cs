using System.Net;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Serialization;

namespace Web.Api.Presenters
{
    public sealed class ObterProfessorInfoPresenter : IOutputPort<GenericDataUseCaseResponse<ProfessorInfo>>
    {
        public JsonContentResult ContentResult { get; }

        public ObterProfessorInfoPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GenericDataUseCaseResponse<ProfessorInfo> response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response.Data);
        }
    }
}
