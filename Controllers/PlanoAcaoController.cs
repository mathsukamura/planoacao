using apiplanoacao.Services.PlanoDeAcao.Interface;
using apiplanoacao.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace apiplanoacao.Controllers
{
    [Authorize]
    [Route("plano-acao")]
    public class PlanoAcaoController : ControllerBase
    {
        private readonly IPlanoAcaoService _planoAcaoService;

        public PlanoAcaoController(IPlanoAcaoService planoAcaoService)
        {
            _planoAcaoService = planoAcaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var plano = await _planoAcaoService.GetAsync();

            return Ok(plano);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(PlanoAcaoViewModel model)
        {
            var plano = await _planoAcaoService.PostAsync(model);

            return Ok(plano.Id);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(PlanoAcaoViewModel model, int id)
        {
            var plano = await _planoAcaoService.PutAsync(model, id);

            return Ok(plano);
        }

        //[HttpPut("/alterar-status")]
        //public async Task<IActionResult> PutStatusAsync(PlanoAcaoViewModel model, int id)
        //{
        //    var plano = await _planoAcaoService.AlterarStatus(model, id);

        //    return Ok(plano.Status);
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var plano = await _planoAcaoService.DeleteAsync(id);

            return Ok(true);
        }
    }
}
