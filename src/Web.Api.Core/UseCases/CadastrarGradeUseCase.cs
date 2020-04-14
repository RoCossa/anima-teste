using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseParams;

using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.Services;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class CadastrarGradeUseCase : ICadastrarGradeUseCase
    {
        private readonly IGradeRepository _gradeRepository;

        public CadastrarGradeUseCase(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<bool> Handle(CadastrarGradeUseCaseParams message, IOutputPort<GenericDataUseCaseResponse<Grade>> outputPort)
        {
            var response = await _gradeRepository.CadastrarGrade(new Domain.Entities.Grade()
            {
                CodGrade = message.Codigo,
                Curso = message.Curso,
                Disciplina = message.Disciplina,
                Turma = message.Turma,
                CodFuncionario = message.CodFuncionario
            });
            outputPort.Handle(response.Success ? new GenericDataUseCaseResponse<Grade>(response.Data, true) : new GenericDataUseCaseResponse<Grade>(response.Errors.Select(e => e.Description)));
            return response.Success;
        }
    }
}
