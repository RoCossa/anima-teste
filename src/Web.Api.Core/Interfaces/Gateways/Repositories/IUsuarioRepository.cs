using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IUsuarioRepository
    {
        Task<DbGenericResponse<Core.Domain.Entities.Usuario>> CriarUsuario(Usuario usuario);
        Task<DbGenericResponse<Core.Domain.Entities.Aluno>> CriarAluno(Core.Domain.Entities.Aluno aluno);
        DbGenericResponse<Core.Domain.Entities.Aluno> ObterAluno(int ra);
        Task<DbGenericResponse<Core.Domain.Entities.Professor>> CriarProfessor(Core.Domain.Entities.Professor professor);
        DbGenericResponse<ProfessorInfo> ObterProfessorInfo(string cpf);
    }
}
