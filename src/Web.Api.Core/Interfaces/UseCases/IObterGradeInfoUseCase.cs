using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseParams;

using Web.Api.Core.Dto.UseCaseResponses;

namespace Web.Api.Core.Interfaces.UseCases
{
  public interface IObterGradeInfoUseCase : IUseCaseRequestHandler<ObterGradeInfoUseCaseParams, GenericDataUseCaseResponse<GradeInfo>>
  {
  }
}
