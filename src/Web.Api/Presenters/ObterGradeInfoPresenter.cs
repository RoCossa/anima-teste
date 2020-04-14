using System.Net;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Serialization;

namespace Web.Api.Presenters
{
    public sealed class ObterGradeInfoPresenter : IOutputPort<GenericDataUseCaseResponse<GradeInfo>>
    {
        public JsonContentResult ContentResult { get; }

        public ObterGradeInfoPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GenericDataUseCaseResponse<GradeInfo> response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response.Data);
        }
    }
}
