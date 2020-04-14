using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Dto.UseCaseParams;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters;

namespace Web.Api.Controllers
{
    [Route("school/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly ICadastrarProfessorUseCase _cadastrarProfessorUseCase;
        private readonly CadastrarProfessorPresenter _cadastrarProfessorPresenter;
        private readonly IObterProfessorInfoUseCase _obterProfessorInfoUseCase;
        private readonly ObterProfessorInfoPresenter _obterProfessorInfoPresenter;

        public ProfessorController(ICadastrarProfessorUseCase cadastrarProfessorUseCase, CadastrarProfessorPresenter cadastrarProfessorPresenter, IObterProfessorInfoUseCase obterProfessorInfoUseCase, ObterProfessorInfoPresenter obterProfessorInfoPresenter)
        {
            _cadastrarProfessorUseCase = cadastrarProfessorUseCase;
            _cadastrarProfessorPresenter = cadastrarProfessorPresenter;
            _obterProfessorInfoUseCase = obterProfessorInfoUseCase;
            _obterProfessorInfoPresenter = obterProfessorInfoPresenter;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.Request.CadastrarProfessorRequest request)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            var useCaseParams = new CadastrarProfessorUseCaseParams(request.Nome, request.Senha, request.Email, request.Login, request.Cpf, request.Codigo);
            await _cadastrarProfessorUseCase.Handle(useCaseParams, _cadastrarProfessorPresenter);
            return _cadastrarProfessorPresenter.ContentResult;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string cpf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _obterProfessorInfoUseCase.Handle(new ObterProfessorInfoUseCaseParams(cpf), _obterProfessorInfoPresenter);
            return _obterProfessorInfoPresenter.ContentResult;
        }
    }
}
