using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseParams
{
    public class ObterGradeInfoUseCaseParams : IUseCaseRequest<GenericDataUseCaseResponse<GradeInfo>>
    {
        public int CodGrade;

        public ObterGradeInfoUseCaseParams(int codGrade)
        {
            CodGrade = codGrade;
        }
    }
}
