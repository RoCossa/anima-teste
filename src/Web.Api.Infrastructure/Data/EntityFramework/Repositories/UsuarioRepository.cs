using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Infrastructure.Data.Entities;


namespace Web.Api.Infrastructure.Data.EntityFramework.Repositories
{
    internal sealed class UsuarioRepository : IUsuarioRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UsuarioRepository(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DbGenericResponse<Core.Domain.Entities.Usuario>> CriarUsuario(Core.Domain.Entities.Usuario usuario)
        {
            try
            {
                if (_context.Usuario.Any(u => u.Cpf == usuario.Cpf))
                {
                    List<Error> errors = new List<Error>();
                    Error error = new Error("500", "Já existe um usuário com este CPF.");
                    errors.Add(error);
                    return new DbGenericResponse<Core.Domain.Entities.Usuario>(null, false, errors);
                }
                var usuarioDb = _mapper.Map<Data.Entities.Usuario>(usuario);
                _context.Usuario.Add(usuarioDb);
                await _context.SaveChangesAsync();
                return new DbGenericResponse<Core.Domain.Entities.Usuario>(_mapper.Map<Core.Domain.Entities.Usuario>(usuarioDb), true, null);
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<Core.Domain.Entities.Usuario>(null, false, errors);
            }
        }

        public async Task<DbGenericResponse<Core.Domain.Entities.Aluno>> CriarAluno(Core.Domain.Entities.Aluno aluno)
        {
            var alunoDb = _mapper.Map<Data.Entities.Aluno>(aluno);
            _context.Aluno.Add(alunoDb);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<Core.Domain.Entities.Aluno>(null, false, errors);
            }

            return new DbGenericResponse<Core.Domain.Entities.Aluno>(_mapper.Map<Core.Domain.Entities.Aluno>(alunoDb), true, null);
        }

        public async Task<DbGenericResponse<Core.Domain.Entities.Professor>> CriarProfessor(Core.Domain.Entities.Professor professor)
        {
            var professorDb = _mapper.Map<Data.Entities.Professor>(professor);
            _context.Professor.Add(professorDb);
            try
            {
                await _context.SaveChangesAsync();
                return new DbGenericResponse<Core.Domain.Entities.Professor>(_mapper.Map<Core.Domain.Entities.Professor>(professorDb), true, null);

            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<Core.Domain.Entities.Professor>(null, false, errors);
            }

        }

        public DbGenericResponse<Core.Domain.Entities.Aluno> ObterAluno(int ra)
        {
            try
            {
                var aluno = _mapper.Map<Core.Domain.Entities.Aluno>(_context.Aluno.FirstOrDefault(a => a.Ra == ra));
                return new DbGenericResponse<Core.Domain.Entities.Aluno>(aluno);
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<Core.Domain.Entities.Aluno>(null, false, errors);
            }

        }

        public DbGenericResponse<ProfessorInfo> ObterProfessorInfo(string cpf)
        {
            try
            {
                var professorInfo = (from u in _context.Usuario
                                     from p in _context.Professor.Where(p => u.IdUsuario == p.IdUsuario)
                                     where u.Cpf == cpf
                                     select new ProfessorInfo()
                                     {
                                         CodFuncionario = p.Codigo,
                                         Cpf = u.Cpf,
                                         Email = u.Email,
                                         Nome = u.Nome,
                                         TotalGrades = (from gd in _context.GradeDetalhe
                                                        from g in _context.Grade.Where(g => g.IdGradeDetalhe == gd.IdGradeDetalhe)
                                                        where gd.IdProfessor == p.IdProfessor
                                                        select g.IdGrade).Distinct().Count(),
                                         TotalAlunos = (from gd in _context.GradeDetalhe
                                                        from g in _context.Grade.Where(g => g.IdGradeDetalhe == gd.IdGradeDetalhe)
                                                        from ag in _context.AlunoGrade.Where(ag => g.IdGrade == ag.IdGrade)
                                                        where gd.IdProfessor == p.IdProfessor
                                                        select ag.IdAluno).Distinct().Count(),
                                     }).FirstOrDefault();
                return new DbGenericResponse<ProfessorInfo>(professorInfo, true);
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                Error error = new Error("500", ex.Message);
                errors.Add(error);
                return new DbGenericResponse<ProfessorInfo>(null, false, errors);
            }
        }
    }
}
