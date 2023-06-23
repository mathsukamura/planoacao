using apiplanoacao.Data;
using apiplanoacao.Models;
using apiplanoacao.Services.CapturaUsuario;
using apiplanoacao.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Numerics;
using apiplanoacao.Services.PlanoDeAcao.Interface;
using System.Globalization;
using System;

namespace apiplanoacao.Services.PlanoDeAcao
{

    public class PlanoAcaoService : IPlanoAcaoService
    {
        private readonly ContextDb _context;
        private readonly IObterUsuariorServices _obterUsuariorServices;

        public PlanoAcaoService(ContextDb context, IObterUsuariorServices obterUsuariorServices) 
        {
            _context = context;
            _obterUsuariorServices = obterUsuariorServices;
        }

        public async Task<IList<PlanoAcaoModel>> GetAsync()
        {
            var usuario = await ValidaUsuario();

            if ( usuario == null)
            {
                return null;
            }

            var planoacao = await _context.PlanoAcoes.
                Where(p => p.IdUsuario == usuario.Id)
                .AsNoTracking()
                .ToListAsync();

            if (planoacao == null)
            {
                return null;
            }

            return (planoacao);

        }

        public async Task<PlanoAcaoModel> PostAsync(PlanoAcaoViewModel model)
        {
            var plano = model.CreatePlano();

            var usuario = await ValidaUsuario();

            var responsaveisTratativa = new List<UsuarioModel>();

            if (model.ResponsaveisTratativa != null && model.ResponsaveisTratativa.Any())
            {
                var responsaveis = await _context.Usuarios
                    .Where(u => model.ResponsaveisTratativa.Contains(u.Id))
                    .ToListAsync();

                responsaveisTratativa.AddRange(responsaveis);
            }

            plano.ResponsaveisTratativa = responsaveisTratativa;

            plano.IdUsuario = usuario.Id;

            await _context.AddAsync(plano);

            await _context.SaveChangesAsync();

            return plano;
        }

        public async Task<PlanoAcaoModel> PutAsync(PlanoAcaoViewModel model, int id)
        {
            var plano = await _context.PlanoAcoes.FindAsync(id);

            if (plano == null)
            {
                return null;
            }

            var usuario = await ValidaUsuario();

            if (plano.IdUsuario != usuario.Id)
            {
                return null;
            }

            plano.AtualizaPlano(model);

            await _context.SaveChangesAsync();

            return plano;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plano = await _context.PlanoAcoes.FirstOrDefaultAsync(x => x.Id == id);

            var usuario = await ValidaUsuario(); 

            if (plano.IdUsuario != usuario.Id)
            {
                return false;
            }

            if(plano == null)
            {
                return false;
            }

            _context.PlanoAcoes.Remove(plano);

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<UsuarioModel> ValidaUsuario()
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == idUsuario);

            return usuario;
        }

        //public async Task<bool> AlterarStatusPlanoAcao(PlanoAcaoModel model ,int id)
        //{
        //    var planoAcao = await _context.PlanoAcoes.FindAsync(id);

        //    if (planoAcao == null)
        //    {
        //        // Plano de ação não encontrado
        //        return false;
        //    }

        //    var usuario = await ValidaUsuario();

        //    // Verifica se o usuário é responsável pela tratativa
        //    if (planoAcao.ResponsavelTratativa != usuario.Id)
        //    {
        //        // Usuário não é o responsável pela tratativa
        //        return false;
        //    }

        //    if (planoAcao.Status == EStatus.Aberto && model.Status == EStatus.EmAndamento)
        //    {
        //        planoAcao.Status = EStatus.EmAndamento;
        //    }

        //    else if (planoAcao.Status == EStatus.EmAndamento && model.Status == EStatus.AguardandoAprovacao)
        //    {
        //        planoAcao.Status = EStatus.AguardandoAprovacao;
        //    }

        //    else if (planoAcao.Status == EStatus.AguardandoAprovacao && (model.Status == EStatus.Concluído || model.Status == EStatus.Reprovado))
        //    {
        //        var colaboradorAprovador = await _context.Usuarios.FindAsync(planoAcao.ColaboradorAprovador);

        //        if (colaboradorAprovador == null || colaboradorAprovador.Id != usuario.Id)
        //        { 
        //            return false;
        //        }

        //        planoAcao.Status = model.Status;
        //    }
        //    else
        //    {
        //        return false;
        //    }


        //    await _context.SaveChangesAsync();

        //    return true;
        //}


    }
}
