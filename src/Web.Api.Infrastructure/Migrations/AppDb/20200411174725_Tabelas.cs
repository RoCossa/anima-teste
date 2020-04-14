using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Api.Infrastructure.Migrations.AppDb
{
    public partial class Tabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.UniqueConstraint("AlternateKey_Cpf", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    IdAluno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ra = table.Column<int>(nullable: false),
                    IdUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.IdAluno);
                    table.ForeignKey(
                        name: "FK_Aluno_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    IdProfessor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(nullable: false),
                    IdUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.IdProfessor);
                    table.ForeignKey(
                        name: "FK_Professor_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeDetalhe",
                columns: table => new
                {
                    IdGradeDetalhe = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Curso = table.Column<string>(nullable: true),
                    Disciplina = table.Column<string>(nullable: true),
                    Turma = table.Column<string>(nullable: true),
                    CodGrade = table.Column<int>(nullable: false),
                    IdProfessor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeDetalhe", x => x.IdGradeDetalhe);
                    table.ForeignKey(
                        name: "FK_GradeDetalhe_Professor_IdProfessor",
                        column: x => x.IdProfessor,
                        principalTable: "Professor",
                        principalColumn: "IdProfessor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    IdGrade = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdGradeDetalhe = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.IdGrade);
                    table.ForeignKey(
                        name: "FK_Grade_GradeDetalhe_IdGradeDetalhe",
                        column: x => x.IdGradeDetalhe,
                        principalTable: "GradeDetalhe",
                        principalColumn: "IdGradeDetalhe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoGrade",
                columns: table => new
                {
                    IdAlunoGrade = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdAluno = table.Column<int>(nullable: false),
                    IdGrade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoGrade", x => x.IdAlunoGrade);
                    table.ForeignKey(
                        name: "FK_AlunoGrade_Grade_IdGrade",
                        column: x => x.IdGrade,
                        principalTable: "Grade",
                        principalColumn: "IdGrade",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_IdUsuario",
                table: "Aluno",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoGrade_IdGrade",
                table: "AlunoGrade",
                column: "IdGrade");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_IdGradeDetalhe",
                table: "Grade",
                column: "IdGradeDetalhe");

            migrationBuilder.CreateIndex(
                name: "IX_GradeDetalhe_IdProfessor",
                table: "GradeDetalhe",
                column: "IdProfessor");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_IdUsuario",
                table: "Professor",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "AlunoGrade");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "GradeDetalhe");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
