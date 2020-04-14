using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseParams
{
    public class CadastrarGradeUseCaseParams : IUseCaseRequest<GenericDataUseCaseResponse<Grade>>
    {
        public int Codigo { get; }
        public string Turma { get; }
        public string Disciplina { get; }
        public string Curso { get; }
        public int CodFuncionario { get; }

        public CadastrarGradeUseCaseParams(int codigo, string turma, string disciplina, string curso, int codFuncionario)
        {
            Codigo = codigo;
            Turma = turma;
            Disciplina = disciplina;
            Curso = curso;
            CodFuncionario = codFuncionario;
        }
    }
}
