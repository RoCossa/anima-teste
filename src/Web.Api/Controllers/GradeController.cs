using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Dto.UseCaseParams;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters;

namespace Web.Api.Controllers
{
    [Route("school/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly ICadastrarGradeUseCase _cadastrarGradeUseCase;
        private readonly CadastrarGradePresenter _cadastrarGradePresenter;
        private readonly IObterGradeInfoUseCase _obterGradeInfoUseCase;
        private readonly ObterGradeInfoPresenter _obterGradeInfoPresenter;

        public GradeController(ICadastrarGradeUseCase cadastrarGradeUseCase, CadastrarGradePresenter cadastrarGradePresenter, IObterGradeInfoUseCase obterGradeInfoUseCase, ObterGradeInfoPresenter obterGradeInfoPresenter)
        {
            _cadastrarGradeUseCase = cadastrarGradeUseCase;
            _cadastrarGradePresenter = cadastrarGradePresenter;
            _obterGradeInfoUseCase = obterGradeInfoUseCase;
            _obterGradeInfoPresenter = obterGradeInfoPresenter;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.Request.CadastrarGradeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var useCaseParams = new CadastrarGradeUseCaseParams(request.CodGrade, request.Turma, request.Disciplina, request.Curso, request.CodFuncionario);
            await _cadastrarGradeUseCase.Handle(useCaseParams, _cadastrarGradePresenter);
            return _cadastrarGradePresenter.ContentResult;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int codGrade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _obterGradeInfoUseCase.Handle(new ObterGradeInfoUseCaseParams(codGrade), _obterGradeInfoPresenter);
            return _obterGradeInfoPresenter.ContentResult;
        }
    }
}
