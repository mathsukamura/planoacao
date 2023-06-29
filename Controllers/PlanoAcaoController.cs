using apiplanoacao.Models;
using apiplanoacao.Models.Enuns;
using apiplanoacao.Services.Notification;
using apiplanoacao.Services.PlanoDeAcao.Interface;
using apiplanoacao.Services.tratativas;
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
        private readonly INotificationService _notificationService;
        private readonly ITratativaService _tratativaService;

        public PlanoAcaoController(IPlanoAcaoService planoAcaoService, IPlanoAcaoRegrasService planoAcaoRegrasService, INotificationService notificationService, ITratativaService tratativaService)
        {
            _planoAcaoService = planoAcaoService;
            _planoAcaoRegrasService = planoAcaoRegrasService;
            _notificationService = notificationService;
            _tratativaService= tratativaService;
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

        [HttpPut("tratamento/{id}")]
        public async Task<IActionResult> AlterarStatusAsync(int id, EAction acao)
        {
            var tratativa = await _tratativaService.AlteraStatus(id, acao);

            if(tratativa == false)
            {
                return BadRequest(_notificationService.GetAllNotifications());
            }

            return Ok(_notificationService.GetAllNotifications());
        }
    }
}
