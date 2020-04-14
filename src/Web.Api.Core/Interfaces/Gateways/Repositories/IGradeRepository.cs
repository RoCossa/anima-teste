using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IGradeRepository
    {
        Task<DbGenericResponse<Core.Domain.Entities.Grade>> CadastrarGrade(Grade grade);
        DbGenericResponse<bool> GradeExists(int codGrade);
        Task<DbGenericResponse<bool>> CadastrarAlunoGrade(int codGrade, int idAluno);
        Task<DbGenericResponse<bool>> DesmatricularAlunoGrade(int codGrade, int ra);
        DbGenericResponse<bool> AlunoGradeExists(int codGrade, int ra);
        DbGenericResponse<GradeInfo> ObterGrade(int condGrade);
    }
}
