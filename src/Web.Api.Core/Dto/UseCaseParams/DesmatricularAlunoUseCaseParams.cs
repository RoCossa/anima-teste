using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseParams
{
    public class DesmatricularAlunoUseCaseParams : IUseCaseRequest<GenericDataUseCaseResponse<bool>>
    {
        public int Ra;
        public int CodGrade;

        public DesmatricularAlunoUseCaseParams(int ra, int codGrade)
        {
           Ra = ra;
           CodGrade = codGrade;
        }
    }
}
