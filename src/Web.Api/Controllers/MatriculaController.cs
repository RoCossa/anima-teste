using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Dto.UseCaseParams;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters;

namespace Web.Api.Controllers
{
    [Route("school/[controller]")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {
        private readonly IMatricularAlunoUseCase _matricularAlunoUseCase;
        private readonly IDesmatricularAlunoUseCase _desmatricularAlunoUseCase;
        private readonly MatricularAlunoPresenter _matricularAlunoPresenter;

        public MatriculaController(IMatricularAlunoUseCase matricularAlunoUseCase, IDesmatricularAlunoUseCase desmatricularAlunoUseCase, MatricularAlunoPresenter matricularAlunoPresenter)
        {
            _matricularAlunoUseCase = matricularAlunoUseCase;
            _desmatricularAlunoUseCase = desmatricularAlunoUseCase;
            _matricularAlunoPresenter = matricularAlunoPresenter;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.Request.MatricularAlunoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var useCaseParams = new MatricularAlunoUseCaseParams(request.Ra, request.CodGrade);
            await _matricularAlunoUseCase.Handle(useCaseParams, _matricularAlunoPresenter);
            return _matricularAlunoPresenter.ContentResult;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] Models.Request.MatricularAlunoRequest request)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            var useCaseParams = new DesmatricularAlunoUseCaseParams(request.Ra, request.CodGrade);
            await _desmatricularAlunoUseCase.Handle(useCaseParams, _matricularAlunoPresenter);
            return _matricularAlunoPresenter.ContentResult;
        }
    }
}
