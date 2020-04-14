using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseParams
{
    public class ObterProfessorInfoUseCaseParams : IUseCaseRequest<GenericDataUseCaseResponse<ProfessorInfo>>
    {
        public string Cpf;

        public ObterProfessorInfoUseCaseParams(string cpf)
        {
            Cpf = cpf;
        }
    }
}
