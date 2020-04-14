using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Api.Infrastructure.Data.Entities;
using Web.Api.Infrastructure.Data.EntityFramework.Entities;


namespace Web.Api.Infrastructure.Data.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasAlternateKey(c => c.Cpf) 
            .HasName("AlternateKey_Cpf");
    }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<GradeDetalhe> GradeDetalhe { get; set; }
        public DbSet<AlunoGrade> AlunoGrade { get; set; }

    }
}
