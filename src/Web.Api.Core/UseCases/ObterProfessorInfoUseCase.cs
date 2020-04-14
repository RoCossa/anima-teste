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
    public sealed class ObterProfessorInfoUseCase : IObterProfessorInfoUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGradeRepository _gradeRepository;

        public ObterProfessorInfoUseCase(IUsuarioRepository usuarioRepository, IGradeRepository gradeRepository)
        {
            _usuarioRepository = usuarioRepository;
            _gradeRepository = gradeRepository;
        }

        public async Task<bool> Handle(ObterProfessorInfoUseCaseParams message, IOutputPort<GenericDataUseCaseResponse<ProfessorInfo>> outputPort)
        {
            List<Error> errors = new List<Error>();

            var response = _usuarioRepository.ObterProfessorInfo(message.Cpf);
            if (response.Data == null)
            {
                Error error = new Error("", "Professor não encontrado");
                errors.Add(error);
            }
            if (errors.Count > 0)
            {
                outputPort.Handle(new GenericDataUseCaseResponse<ProfessorInfo>(errors.Select(e => e.Description), data: null));
                return false;
            }
            else
            {
                response.Data.Salario = CalcularSalario(response.Data);
                outputPort.Handle(response.Success ? new GenericDataUseCaseResponse<ProfessorInfo>(response.Data, true) : new GenericDataUseCaseResponse<ProfessorInfo>(response.Errors?.Select(e => e.Description)));
                return response.Success;
            }
        }

        private double CalcularSalario(ProfessorInfo professor)
        {
            return ((((double)professor.TotalAlunos / 10) * (double)professor.TotalGrades) * 50) + 1200;
        }
    }
}
