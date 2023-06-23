using apiplanoacao.Data;
using apiplanoacao.Models;
using apiplanoacao.Services.CapturaUsuario;
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
        public PlanosAcaoRegrasService(ContextDb context, IObterUsuariorServices obterUsuariorServices )
        {
            _context = context;
            _obterUsuariorServices = obterUsuariorServices;
        }

        public async Task<IList<PlanoAcaoModel>> GetTratativasPentendesAsync(EStatus? status, DateTime? dataInicio, DateTime? dataFim)
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var responsavel = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == idUsuario);

            if (responsavel == null)
            {
                return null;
            }

            var query = _context.PlanoAcoes.Where(p => p.ResponsavelTratativa == responsavel.Id);

            if (status != null)
            {
                query = query.Where(p => p.Status == status);
            }

            if (dataInicio != null)
            {
                query = query.Where(p => p.DataInicio >= dataInicio);
            }

            if (dataFim != null)
            {
                query = query.Where(p => p.DataFim <= dataFim);
            }

            var planoAcao = await query.AsNoTracking().ToListAsync();

            if (planoAcao == null)
            {
                return null;
            }

            return planoAcao;
        }

        public async Task<IList<PlanoAcaoModel>> GetAprovacoesPendentesAsync(EStatus? status, DateTime? dataInicio, DateTime? dataFim)
        {
            var usuarioId = _obterUsuariorServices.ObterUsuarioId();

            var planosAcao = _context.PlanoAcoes.Where(p => p.ColaboradorId == usuarioId);

            if (status != null)
            {
                planosAcao = planosAcao.Where(p => p.Status == status);
            }

            if (dataInicio != null)
            {
                planosAcao = planosAcao.Where(p => p.DataInicio >= dataInicio);
            }

            if (dataFim != null)
            {
                planosAcao = planosAcao.Where(p => p.DataFim <= dataFim);
            }

            var aprovacoesPendentes = await planosAcao.AsNoTracking().ToListAsync();

            return aprovacoesPendentes;
        }

        public async Task<bool> AlterarStatusPlanoAcaoCompleto(List<int> ids, EStatus novoStatus)
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var responsavel = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == idUsuario);

            if (responsavel == null)
            {
                return false;
            }

            var planosAcao = await _context.PlanoAcoes
                .Where(p => ids.Contains(p.Id) && p.ResponsavelTratativa == responsavel.Id)
                .ToListAsync();

            if (planosAcao == null || planosAcao.Count == 0)
            {
                return false;
            }

            foreach (var planoAcao in planosAcao)
            {
                if (novoStatus == EStatus.Concluído || novoStatus == EStatus.Reprovado)
                {
                    var colaboradorAprovador = await _context.Usuarios.FindAsync(planoAcao.ColaboradorAprovador);

                    if (colaboradorAprovador == null || colaboradorAprovador.Id != responsavel.Id)
                    {
                        return false;
                    }
                }

                if (planoAcao.Status == EStatus.Aberto && novoStatus == EStatus.EmAndamento)
                {
                    planoAcao.Status = EStatus.EmAndamento;
                }
                else if (planoAcao.Status == EStatus.Aberto && novoStatus == EStatus.AguardandoAprovacao)
                {
                    planoAcao.Status = EStatus.AguardandoAprovacao;
                }
                else if (novoStatus == EStatus.Concluído || novoStatus == EStatus.Reprovado)
                {
                    planoAcao.Status = novoStatus;
                }
                else
                {
                    return false;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AlterarStatusPlanoAcao(List<int> ids, EStatus novoStatus)
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var responsavel = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == idUsuario);

            if (responsavel == null)
            {
                return false;
            }

            var planosAcao = await _context.PlanoAcoes
                .Where(p => ids.Contains(p.Id) && p.ResponsavelTratativa == responsavel.Id)
                .ToListAsync();

            if (planosAcao == null || planosAcao.Count == 0)
            {
                return false;
            }

            foreach (var planoAcao in planosAcao)
            {
                if (planoAcao.Status == EStatus.Aberto && novoStatus == EStatus.EmAndamento)
                {
                    planoAcao.Status = EStatus.EmAndamento;
                }
                else if (planoAcao.Status == EStatus.Aberto && novoStatus == EStatus.AguardandoAprovacao)
                {
                    planoAcao.Status = EStatus.AguardandoAprovacao;
                }
                else
                {
                    return false;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }


    }
}
