
namespace Web.Api.Core.Domain.Entities
{
    public class Grade
    {
        public int IdGrade { get; set; }
        public string Curso { get; set; }
        public string Disciplina { get; set; }
        public string Turma { get; set; }
        public int CodGrade { get; set; }
        public int CodFuncionario { get; set; }
    }
}
