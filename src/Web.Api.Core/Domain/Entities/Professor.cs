namespace Web.Api.Core.Domain.Entities
{
    public class Professor
    {
        public int IdProfessor { get; set; }
        public int Codigo { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
