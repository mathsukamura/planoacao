using apiplanoacao.Data;
using apiplanoacao.Models;
using apiplanoacao.Services.CapturaUsuario;
using apiplanoacao.Services.Notification;
using apiplanoacao.Services.PlanoDeAcao.Interface;
using apiplanoacao.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiplanoacao.Services.PlanoDeAcao
{
    public class PlanosAcaoRegrasService : IPlanoAcaoRegrasService
    {
        private readonly ContextDb _context;

        private readonly IObterUsuariorServices _obterUsuariorServices;
        private readonly INotificationService _notificationService;

        public PlanosAcaoRegrasService(ContextDb context, IObterUsuariorServices obterUsuariorServices, INotificationService notificationService)
        {
            _context = context;
            _obterUsuariorServices = obterUsuariorServices;
            _notificationService = notificationService;
        }

        public async Task<IList<PlanoAcaoModel>> GetTratativasPentendesAsync(EStatus? status, DateTime? dataInicio, DateTime? dataFim)
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var busca = _context.PlanoAcoes
                .Where(p => p.ResponsaveisTratativa.Any(r => r.Id == idUsuario) || p.ColaboradorAprovador.Id == idUsuario);

            busca = FiltroDePlanosDeAcao(busca, status, dataInicio, dataFim);

            var planoAcao = await busca.AsNoTracking().ToListAsync();

            if (planoAcao == null)
            {
                return null;
            }

            return planoAcao;
        }

        public async Task<bool> AlterarStatusPlanoAcao(int id, EStatus novoStatus)
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var planoAcao = await ObterPlanoAcaoPorIdEUsuario(id, idUsuario);

            if (planoAcao == null)
            {
                _notificationService.AddNotification("message", "Plano de ação não existe ou você não tem altorização");

                return false;
            }

            var statusValido = AlterarStatus(planoAcao, novoStatus, idUsuario);

            if (statusValido)
            {
                _notificationService.AddNotification("message", $"O plano de ação foi alterado para {novoStatus}");
            }
            else
            {
                _notificationService.AddNotification("message", $"Não é possível alterar o status do plano de ação de ´{planoAcao.Status}' para o status desejado.");

                return false;
            }

            AtualizarStatusPlanoAcao(planoAcao, novoStatus);

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<PlanoAcaoModel> ObterPlanoAcaoPorIdEUsuario(int id, int idUsuario)
        {
            return await _context.PlanoAcoes
                .FirstOrDefaultAsync(p => p.Id == id && (p.ResponsaveisTratativa.Any(r => r.Id == idUsuario) || p.ColaboradorId == idUsuario));
        }

        private bool AlterarStatus(PlanoAcaoModel planoAcao, EStatus novoStatus, int idUsuario)
        {
            if (planoAcao.StatusAtualExigeColaboradorResponsavel(idUsuario) == false)
            {
                return false;
            }

            if (planoAcao.Status == EStatus.Aberto)
            {
                if (novoStatus == EStatus.EmAndamento)
                {
                    return true;
                }

                return false;
            }

            if (planoAcao.Status == EStatus.EmAndamento)
            {
                if (novoStatus == EStatus.AguardandoAprovacao)
                {
                    return true;
                }

                return false;
            }

            if (planoAcao.Status == EStatus.AguardandoAprovacao)
            {
                if (PermiteAprovarOuReprovarPlano(planoAcao, novoStatus, idUsuario))
                {
                    return true;
                }

                return false;
            }

            return false;
        }
        private bool PermiteAprovarOuReprovarPlano(PlanoAcaoModel planoAcao, EStatus novoStatus, int idUsuario)
        {
            return (planoAcao.Status == EStatus.AguardandoAprovacao &&
                (novoStatus == EStatus.Concluído || novoStatus == EStatus.Reprovado) &&
                (planoAcao.ColaboradorId == idUsuario));
        }

        private void AtualizarStatusPlanoAcao(PlanoAcaoModel planoAcao, EStatus novoStatus)
        {
            planoAcao.Status = novoStatus;
        }

        private IQueryable<PlanoAcaoModel> FiltroDePlanosDeAcao(IQueryable<PlanoAcaoModel> busca, EStatus? status, DateTime? dataInicio, DateTime? dataFim)
        {
            if (status != null)
            {
                busca = busca.Where(p => p.Status == status);
            }

            if (dataInicio != null)
            {
                busca = busca.Where(p => p.DataInicio >= dataInicio);
            }

            if (dataFim != null)
            {
                busca = busca.Where(p => p.DataFim <= dataFim);
            }

            return busca;
        }
    }
}
