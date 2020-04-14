using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Interfaces.Gateways.Repositories;


namespace Web.Api.Infrastructure.Data.EntityFramework.Repositories
{
    internal sealed class GradeRepository : IGradeRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public GradeRepository(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public DbGenericResponse<bool> AlunoGradeExists(int codGrade, int ra)
        {
            try
            {
                var t = (from gd in _context.GradeDetalhe
                         from g in _context.Grade.Where(g => gd.IdGradeDetalhe == g.IdGradeDetalhe)
                         from ag in _context.AlunoGrade.Where(ag => g.IdGrade == ag.IdGrade)
                         from a in _context.Aluno.Where(a => ag.IdAluno == a.IdAluno)
                         where gd.CodGrade == codGrade && a.Ra == ra
                         select ag).ToList();
                var exists = (from gd in _context.GradeDetalhe
                              from g in _context.Grade.Where(g => gd.IdGradeDetalhe == g.IdGradeDetalhe)
                              from ag in _context.AlunoGrade.Where(ag => g.IdGrade == ag.IdGrade)
                              from a in _context.Aluno.Where(a => ag.IdAluno == a.IdAluno)
                              where gd.CodGrade == codGrade && a.Ra == ra
                              select ag).Any();

                return new DbGenericResponse<bool>(exists, true);
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<bool>(false, false, errors);
            }
        }

        public async Task<DbGenericResponse<bool>> CadastrarAlunoGrade(int codGrade, int idAluno)
        {
            try
            {
                var listGrade = (from g in _context.Grade
                                 join gd in _context.GradeDetalhe on g.IdGradeDetalhe equals gd.IdGradeDetalhe
                                 where gd.CodGrade == codGrade
                                 select g.IdGrade).ToList();
                foreach (var grade in listGrade)
                {
                    if (_context.AlunoGrade.Where(ag => ag.IdGrade == grade).Count() < 10)
                    {
                        _context.AlunoGrade.Add(new Data.Entities.AlunoGrade()
                        {
                            IdAluno = idAluno,
                            IdGrade = grade
                        });
                        await _context.SaveChangesAsync();
                        return new DbGenericResponse<bool>(true, true);
                    }
                }

                var gradeDb = new Data.Entities.Grade()
                {
                    IdGradeDetalhe = _context.GradeDetalhe.Where(gd => gd.CodGrade == codGrade).Select(gd => gd.IdGradeDetalhe).FirstOrDefault()
                };
                _context.Grade.Add(gradeDb);
                _context.AlunoGrade.Add(new Data.Entities.AlunoGrade()
                {
                    IdAluno = idAluno,
                    IdGrade = gradeDb.IdGrade
                });
                await _context.SaveChangesAsync();
                return new DbGenericResponse<bool>(true, true);

                /*List<Error> errors = new List<Error>();
                Error error = new Error("", "Nenhuma grade encontrada para o código informado.");
                errors.Add(error);
                return new DbGenericResponse<bool>(false, false, errors);*/
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<bool>(false, false, errors);
            }
        }

        public async Task<DbGenericResponse<Core.Domain.Entities.Grade>> CadastrarGrade(Core.Domain.Entities.Grade grade)
        {
            try
            {
                var professorDb = _context.Professor.FirstOrDefault(p => p.Codigo == grade.CodFuncionario);
                if (professorDb == null)
                {
                    List<Error> errors = new List<Error>();
                    Error error = new Error("500", "Código de funcionário não encontrado");
                    errors.Add(error);
                    return new DbGenericResponse<Core.Domain.Entities.Grade>(null, false, errors);
                }
                if (_context.GradeDetalhe.Any(gd => gd.CodGrade == grade.CodGrade))
                {
                    List<Error> errors = new List<Error>();
                    Error error = new Error("500", "Já existe uma grade com este código.");
                    errors.Add(error);
                    return new DbGenericResponse<Core.Domain.Entities.Grade>(null, false, errors);
                }
                else
                {
                    var gradeDetalheDb = new Data.Entities.GradeDetalhe()
                    {
                        CodGrade = grade.CodGrade,
                        Curso = grade.Curso,
                        Disciplina = grade.Disciplina,
                        Turma = grade.Turma,
                        IdProfessor = professorDb.IdProfessor,
                    };
                    _context.GradeDetalhe.Add(gradeDetalheDb);
                    var gradeDb = new Data.Entities.Grade()
                    {
                        IdGradeDetalhe = gradeDetalheDb.IdGradeDetalhe
                    };
                    _context.Grade.Add(gradeDb);
                    await _context.SaveChangesAsync();
                    return new DbGenericResponse<Core.Domain.Entities.Grade>(
                        new Core.Domain.Entities.Grade()
                        {
                            CodFuncionario = gradeDetalheDb.Professor.Codigo,
                            CodGrade = gradeDetalheDb.CodGrade,
                            Curso = gradeDetalheDb.Curso,
                            Disciplina = gradeDetalheDb.Disciplina,
                            Turma = gradeDetalheDb.Turma,
                            IdGrade = gradeDb.IdGrade
                        }
                        , true, null);
                }
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<Core.Domain.Entities.Grade>(null, false, errors);
            }
        }

        public async Task<DbGenericResponse<bool>> DesmatricularAlunoGrade(int codGrade, int ra)
        {
            try
            {
                var alunoGradeDb = (from gd in _context.GradeDetalhe
                                    from g in _context.Grade.Where(g => gd.IdGradeDetalhe == g.IdGrade)
                                    from ag in _context.AlunoGrade.Where(ag => g.IdGrade == ag.IdGrade)
                                    from a in _context.Aluno.Where(a => ag.IdAluno == a.IdAluno)
                                    where gd.CodGrade == codGrade && a.Ra == ra
                                    select ag).FirstOrDefault();
                if (alunoGradeDb == null)
                {
                    return new DbGenericResponse<bool>(false, false);
                }
                _context.AlunoGrade.Remove(alunoGradeDb);
                await _context.SaveChangesAsync();
                return new DbGenericResponse<bool>(true, true);
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<bool>(false, false, errors);
            }
        }

        public DbGenericResponse<bool> GradeExists(int codGrade)
        {
            try
            {
                return new DbGenericResponse<bool>(_context.GradeDetalhe.Any(a => a.CodGrade == codGrade));
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<bool>(false, false, errors);
            }
        }

        public DbGenericResponse<GradeInfo> ObterGrade(int codGrade)
        {
            try
            {
                var gradeInfo = (from gd in _context.GradeDetalhe
                                 from p in _context.Professor.Where(p => gd.IdProfessor == p.IdProfessor)
                                 from u in _context.Usuario.Where(u => p.IdUsuario == u.IdUsuario)
                                 from g in _context.Grade.Where(g => gd.IdGradeDetalhe == g.IdGradeDetalhe)
                                 where gd.CodGrade == codGrade
                                 select new GradeInfo()
                                 {
                                     CodFuncionario = p.Codigo,
                                     CodGrade = gd.CodGrade,
                                     CpfProfessor = u.Cpf,
                                     Curso = gd.Curso,
                                     Disciplina = gd.Disciplina,
                                     EmailProfessor = u.Email,
                                     NomeProfessor = u.Nome,
                                     Turma = gd.Turma,
                                     Alunos = (from ag in _context.AlunoGrade
                                               from gr in _context.Grade.Where(gr => ag.IdGrade == ag.IdGrade)
                                               from grd in _context.GradeDetalhe.Where(grd => gr.IdGradeDetalhe == grd.IdGradeDetalhe)
                                               from a in _context.Aluno.Where(a => ag.IdAluno == a.IdAluno)
                                               from ua in _context.Usuario.Where(ua => ua.IdUsuario == a.IdUsuario)
                                               where grd.CodGrade == codGrade
                                               select new AlunoInfo()
                                               {
                                                   email = ua.Email,
                                                   Nome = ua.Nome,
                                                   Ra = a.Ra
                                               }).Distinct().ToList()

                                 }).FirstOrDefault();

                return new DbGenericResponse<GradeInfo>(gradeInfo, true);
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<GradeInfo>(null, false, errors);
            }

            throw new NotImplementedException();
        }
    }
}
