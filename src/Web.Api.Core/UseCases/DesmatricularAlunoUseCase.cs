using System.Collections.Generic;
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
    public sealed class DesmatricularAlunoUseCase : IDesmatricularAlunoUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGradeRepository _gradeRepository;

        public DesmatricularAlunoUseCase(IUsuarioRepository usuarioRepository, IGradeRepository gradeRepository)
        {
            _usuarioRepository = usuarioRepository;
            _gradeRepository = gradeRepository;
        }

        public async Task<bool> Handle(DesmatricularAlunoUseCaseParams message, IOutputPort<GenericDataUseCaseResponse<bool>> outputPort)
        {
            List<Error> errors = new List<Error>();


            var alunoResponse = _usuarioRepository.ObterAluno(message.Ra);
            if (alunoResponse.Data == null)
            {
                Error error = new Error("", "Aluno não encontrado");
                errors.Add(error);

            }
            var gradeResponse = _gradeRepository.GradeExists(message.CodGrade);
            if (!gradeResponse.Data)
            {
                Error error = new Error("", "Grade não encontrada");
                errors.Add(error);
            }
            if (errors.Count > 0)
            {
                outputPort.Handle(new GenericDataUseCaseResponse<bool>(errors.Select(e => e.Description), data: false));
                return false;
            }
            else
            {
                var responseDesmatricula = await _gradeRepository.DesmatricularAlunoGrade(message.CodGrade, message.Ra);
                if (!responseDesmatricula.Success && responseDesmatricula.Errors == null)
                {
                    Error error = new Error("", "Matrícula não encontrada");
                    errors.Add(error);
                    outputPort.Handle(new GenericDataUseCaseResponse<bool>(errors.Select(e => e.Description), data: false));
                    return false;
                }
                outputPort.Handle(responseDesmatricula.Success ? new GenericDataUseCaseResponse<bool>(true, true) : new GenericDataUseCaseResponse<bool>(responseDesmatricula.Errors?.Select(e => e.Description)));
                return responseDesmatricula.Success;
            }
        }
    }
}
