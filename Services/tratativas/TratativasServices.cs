using apiplanoacao.Data;
using apiplanoacao.Models;
using apiplanoacao.Services.CapturaUsuario;
using apiplanoacao.Services.Notification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using apiplanoacao.Models.Enuns;
using System.Threading.Tasks;

namespace apiplanoacao.Services.tratativas
{

    public interface ITratativaService
    {
        public Task<bool> AlteraStatus(int id, EAction acao);
    }

    public class TratativasServices : ITratativaService
    {
        private readonly ContextDb _context;

        private readonly IObterUsuariorServices _obterUsuariorServices;

        private readonly INotificationService _notificationService;


        public TratativasServices(ContextDb context, IObterUsuariorServices obterUsuariorServices, INotificationService notificationService)
        {
            _obterUsuariorServices = obterUsuariorServices;
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<bool> AlteraStatus(int id, EAction acao)
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var planoAcao = await ValidaPlano(id);

            if (planoAcao == null)
            {
                _notificationService.AddNotification("message", "O plano de ação não existe");

                return false;
            }

            if(TratarPlano(planoAcao, acao, idUsuario))
            {
                await _context.SaveChangesAsync();

                _notificationService.AddNotification("message", $"O plano de ação foi alterado para {planoAcao.Status}");

               
                return true;
            }

            _notificationService.AddNotification("message", "O plano de ação não pôde ser alterado");

            return false;
        }

        private bool TratarPlano(PlanoAcaoModel planoAcao, EAction acao, int idUsuario)
        {
            if (planoAcao.Status == EStatus.Aberto ||
                planoAcao.Status == EStatus.EmAndamento ||
                planoAcao.Status == EStatus.Reprovado)
            {
                if (!planoAcao.ResponsaveisTratativa.Any(r => r.Id == idUsuario))
                {

                    return false;
                }
                
                if (acao == EAction.iniciar)
                {
                    planoAcao.Status = EStatus.EmAndamento;
                    return true;
                }
                else
                if (acao == EAction.finalizar && planoAcao.Status != EStatus.Aberto)
                {
                    planoAcao.Status = EStatus.AguardandoAprovacao;
                    return true;
                }
            }

            if (planoAcao.Status == EStatus.AguardandoAprovacao)
            {
                if (planoAcao.ColaboradorId != idUsuario)
                {

                    return false;
                }

                if (acao == EAction.aprovar)
                {
                    planoAcao.Status = EStatus.Concluído;

                    return true;
                }
                
                if (acao == EAction.reprovar)
                {
                    planoAcao.Status = EStatus.Reprovado;

                    return true;
                }
            }

            return false;
        }

        private async Task<PlanoAcaoModel> ValidaPlano(int id)
        {
            return await _context.PlanoAcoes.Include(p => p.ResponsaveisTratativa)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
