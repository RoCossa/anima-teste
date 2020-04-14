

namespace Web.Api.Models.Request
{
    public class CadastrarGradeRequest
    {
        public int CodGrade { get; set; }
        public string Turma { get; set; }
        public string Disciplina { get; set; }
        public string Curso { get; set; }
        public int CodFuncionario { get; set; }
    }
}
