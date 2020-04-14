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
    public sealed class ObterGradeInfoUseCase : IObterGradeInfoUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGradeRepository _gradeRepository;

        public ObterGradeInfoUseCase(IUsuarioRepository usuarioRepository, IGradeRepository gradeRepository)
        {
            _usuarioRepository = usuarioRepository;
            _gradeRepository = gradeRepository;
        }

        public async Task<bool> Handle(ObterGradeInfoUseCaseParams message, IOutputPort<GenericDataUseCaseResponse<GradeInfo>> outputPort)
        {
            List<Error> errors = new List<Error>();

            var response = _gradeRepository.ObterGrade(message.CodGrade);
            if(response.Data == null){
                Error error = new Error("", "Grade não encontrada");
                errors.Add(error);
            }
            if (errors.Count > 0)
            {
                outputPort.Handle(new GenericDataUseCaseResponse<GradeInfo>(errors.Select(e => e.Description), data: null));
                return false;
            }
            else
            {
                outputPort.Handle(response.Success ? new GenericDataUseCaseResponse<GradeInfo>(response.Data, true) : new GenericDataUseCaseResponse<GradeInfo>(response.Errors?.Select(e => e.Description)));
                return response.Success;
            }
        }
    }
}
