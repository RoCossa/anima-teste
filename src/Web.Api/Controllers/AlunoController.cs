using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Dto.UseCaseParams;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters;

namespace Web.Api.Controllers
{
    [Route("school/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly ICadastrarAlunoUseCase _cadastrarAlunoUseCase;
        private readonly CadastrarAlunoPresenter _cadastrarAlunoPresenter;

        public AlunoController(ICadastrarAlunoUseCase cadastrarAlunoUseCase, CadastrarAlunoPresenter cadastrarAlunoPresenter)
        {
            _cadastrarAlunoUseCase = cadastrarAlunoUseCase;
            _cadastrarAlunoPresenter = cadastrarAlunoPresenter;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.Request.CadastrarAlunoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var useCaseParams = new CadastrarAlunoUseCaseParams(request.Nome, request.Senha, request.Email, request.Login, request.Cpf, request.Ra);
            await _cadastrarAlunoUseCase.Handle(useCaseParams, _cadastrarAlunoPresenter);
            return _cadastrarAlunoPresenter.ContentResult;
        }
    }
}
