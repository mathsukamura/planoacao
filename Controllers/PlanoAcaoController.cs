using apiplanoacao.Models;
using apiplanoacao.Services.PlanoDeAcao.Interface;
using apiplanoacao.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiplanoacao.Controllers
{
    [Authorize]
    [Route("plano-acao")]
    [ApiController]
    public class PlanoAcaoController : ControllerBase
    {
        private readonly IPlanoAcaoService _planoAcaoService;

        private readonly IPlanoAcaoRegrasService _planoAcaoRegrasService;

        public PlanoAcaoController(IPlanoAcaoService planoAcaoService, IPlanoAcaoRegrasService planoAcaoRegrasService)
        {
            _planoAcaoService = planoAcaoService;
            _planoAcaoRegrasService = planoAcaoRegrasService;
        }

        [HttpGet("meus-planos")]
        public async Task<IActionResult> GetAsync()
        {
            var plano = await _planoAcaoService.GetAsync();

            if(plano == null)
            {
                return NotFound("Não existe plano");
            }

            return Ok(plano);
        }

        [HttpPost("Criar-planos")]
        public async Task<IActionResult> PostAsync(PlanoAcaoViewModel model)
        {
            var plano = await _planoAcaoService.PostAsync(model);

            return Ok(plano.Id);
        }

        [HttpPut("alterar-plano/{id}")]
        public async Task<IActionResult> PutAsync(PlanoAcaoViewModel model, int id)
        {
            var plano = await _planoAcaoService.PutAsync(model, id);

            return Ok(plano);
        }

        [HttpDelete("deletar-plano")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var plano = await _planoAcaoService.DeleteAsync(id);

            return Ok(true);
        }

        [HttpGet("tratativas-pendentes")]
        public async Task<IList<PlanoAcaoModel>> GetTratativasPentendesAsync(EStatus? status, DateTime? dataInicio, DateTime? dataFim)
        {
            var tratativas = await _planoAcaoRegrasService.GetTratativasPentendesAsync(status, dataInicio, dataFim);

            return tratativas;
        }

        [HttpGet("aprovacoes-pendentes")]
        public async Task<IList<PlanoAcaoModel>> GetAprovacoesPendentesAsync(EStatus? status, DateTime? dataInicio, DateTime? dataFim) 
        { 
            var aprovacoes = await _planoAcaoRegrasService.GetAprovacoesPendentesAsync( status, dataInicio, dataFim);

            return aprovacoes;
        }


        [HttpPut("Alterar-tratativas")]
        public async Task<bool> AlterarStatusPlanoAcaoCompleto(List<int> ids, EStatus novoStatus)
        {
            var tratativas = await _planoAcaoRegrasService.AlterarStatusPlanoAcaoCompleto(ids, novoStatus);

            return tratativas;
        }

    }
}
