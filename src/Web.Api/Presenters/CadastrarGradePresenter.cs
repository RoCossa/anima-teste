using System.Net;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Serialization;

namespace Web.Api.Presenters
{
    public sealed class CadastrarGradePresenter : IOutputPort<GenericDataUseCaseResponse<Grade>>
    {
        public JsonContentResult ContentResult { get; }

        public CadastrarGradePresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GenericDataUseCaseResponse<Grade> response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
