using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseParams
{
    public class MatricularAlunoUseCaseParams : IUseCaseRequest<GenericDataUseCaseResponse<bool>>
    {
        public int Ra;
        public int CodGrade;

        public MatricularAlunoUseCaseParams(int ra, int codGrade)
        {
           Ra = ra;
           CodGrade = codGrade;
        }
    }
}
