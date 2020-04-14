
using System.Collections.Generic;

namespace Web.Api.Core.Domain.Entities
{
    public class GradeInfo
    {
        public int CodGrade { get; set; }
        public string Turma { get; set; }
        public string Disciplina { get; set; }
        public string Curso { get; set; }
        public int CodFuncionario { get; set; }
        public string NomeProfessor { get; set; }
        public string CpfProfessor { get; set; }
        public string EmailProfessor { get; set; }
        public List<AlunoInfo> Alunos { get; set; }
    }

    public class AlunoInfo
    {
        public string Nome { get; set; }
        public int Ra { get; set; }
        public string email { get; set; }
    }
}
