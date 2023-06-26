using apiplanoacao.Models;
using apiplanoacao.Services.PlanoDeAcao.Interface;
using apiplanoacao.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("")]
        public async Task<IActionResult> GetAsync()
        {
            var plano = await _planoAcaoService.GetAsync();

            return Ok(plano);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plano = await _planoAcaoService.GetById(id);

            if (plano == null)
            {
                return NotFound("Esse plano de ação não existe ou você não tem permissão de acesso.");
            }

            return Ok(plano);
        }

        [HttpPost("")]
        public async Task<IActionResult> PostAsync(PlanoAcaoViewModel model)
        {
            var plano = await _planoAcaoService.PostAsync(model);

            return Ok(plano.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(PlanoAcaoViewModel model, int id)
        {
            var plano = await _planoAcaoService.PutAsync(model, id);

            if (plano != null)
            {
                var mensagem = $"O plano de ação {id} foi alterado com sucesso.";
                return Ok(mensagem);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var plano = await _planoAcaoService.DeleteAsync(id);

            if (plano)
            {
                var mensagem = $"O plano de ação {id} foi deletado com sucesso.";
                return Ok(mensagem);
            }

            return NotFound();
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

        [HttpPut("alterar-status/{id}")]
        public async Task<IActionResult> AlterarStustusasync(int id, EStatus novoStatus)
        {
            var Tratativa = await _planoAcaoRegrasService.AlterarStatusPlanoAcao(id, novoStatus);

            if(Tratativa== null)
            {
                return BadRequest("não é possivel alterar");
            }

            return Ok(Tratativa);
        }
    }
}
