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

            var query = _context.PlanoAcoes
                .Where(p => p.ResponsaveisTratativa.Contains(responsavel));

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

        //public async Task<bool> AlterarStatusPlanoAcaoCompleto(List<int> ids, EStatus novoStatus)
        //{
        //    var idUsuario = _obterUsuariorServices.ObterUsuarioId();

        //    var responsavel = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == idUsuario);

        //    if (responsavel == null)
        //    {
        //        return false;
        //    }

        //    var planosAcao = await _context.PlanoAcoes
        //        .Include(p => p.ResponsaveisTratativa)
        //        .Where(p => ids.Contains(p.Id) && p.ResponsaveisTratativa.Contains(responsavel))
        //        .ToListAsync();

        //    if (planosAcao == null || planosAcao.Count == 0)
        //    {
        //        return false;
        //    }

        //    foreach (var planoAcao in planosAcao)
        //    {
        //        if (novoStatus == EStatus.Concluído || novoStatus == EStatus.Reprovado)
        //        {
        //            var colaboradorAprovador = await _context.Usuarios.FindAsync(planoAcao.ColaboradorAprovador);

        //            if (colaboradorAprovador == null || colaboradorAprovador.Id != responsavel.Id)
        //            {
        //                return false;
        //            }
        //        }

        //        if (planoAcao.Status == EStatus.Aberto && novoStatus == EStatus.EmAndamento)
        //        {
        //            planoAcao.Status = EStatus.EmAndamento;
        //        }
        //        else if (planoAcao.Status == EStatus.Aberto && novoStatus == EStatus.AguardandoAprovacao)
        //        {
        //            planoAcao.Status = EStatus.AguardandoAprovacao;
        //        }
        //        else if (novoStatus == EStatus.Concluído || novoStatus == EStatus.Reprovado)
        //        {
        //            planoAcao.Status = novoStatus;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    await _context.SaveChangesAsync();

        //    return true;
        //}

        public async Task<AlterarStatusPlanoAcaoResult> AlterarStatusPlanoAcao(int id, EStatus novoStatus)
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var responsavel = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == idUsuario);

            if (responsavel == null)
            {
                return new AlterarStatusPlanoAcaoResult { Sucesso = false, Mensagem = "Responsável não encontrado" };
            }

            var planoAcao = await ObterPlanoAcaoPorIdEUsuario(id, responsavel);

            if (planoAcao == null)
            {
                return new AlterarStatusPlanoAcaoResult { Sucesso = false, Mensagem = "Plano de ação não existe" };
            }

            if (!AlterarStatus(planoAcao, novoStatus, idUsuario))
            {
                return new AlterarStatusPlanoAcaoResult { Sucesso = false, Mensagem = "Erro ao alterar status do plano de ação" };
            }

            AtualizarStatusPlanoAcao(planoAcao, novoStatus);

            await _context.SaveChangesAsync();

            return new AlterarStatusPlanoAcaoResult { Sucesso = true, Mensagem = $"O plano de ação {id} foi alterado para {novoStatus}" };
        }

        public class AlterarStatusPlanoAcaoResult
        {
            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
        }

        private async Task<PlanoAcaoModel> ObterPlanoAcaoPorIdEUsuario(int id, UsuarioModel responsavel)
        {
            
            return await _context.PlanoAcoes
                .FirstOrDefaultAsync(p => p.Id == id && p.ResponsaveisTratativa.Contains(responsavel));

            
        }

        private bool AlterarStatus(PlanoAcaoModel planoAcao, EStatus novoStatus, int idUsuario)
        {
            if (planoAcao.Status == EStatus.Aberto && novoStatus == EStatus.EmAndamento)
            {
                return true;
            }

            if (planoAcao.Status == EStatus.EmAndamento &&
                novoStatus == EStatus.AguardandoAprovacao)
            {
                return true;
            }

            if (PermiteAprovarOuReprovarPlano(planoAcao, novoStatus, idUsuario))
            {
                return true;
            }

            return false;
        }

        private void AtualizarStatusPlanoAcao(PlanoAcaoModel planoAcao, EStatus novoStatus)
        {
            planoAcao.Status = novoStatus;
        }

        private bool PermiteAprovarOuReprovarPlano(PlanoAcaoModel planoAcao, EStatus novoStatus, int idUsuario)
        {
            return (planoAcao.Status == EStatus.AguardandoAprovacao &&
                (novoStatus == EStatus.Concluído || novoStatus == EStatus.Reprovado) &&
                (planoAcao.ColaboradorAprovador.Id == idUsuario));
        }
    }
}
