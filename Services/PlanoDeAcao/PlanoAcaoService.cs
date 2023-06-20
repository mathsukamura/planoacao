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
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == idUsuario);

            if (usuario == null)
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

            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == idUsuario);

            if (usuario == null)
            {
                return null;
            }

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

            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == idUsuario);

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

            var usuario = await ValidaColaborador(); 

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

        private async Task<UsuarioModel> ValidaColaborador()
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == idUsuario);

            return usuario;
        }

        public  void AlterarStatus(PlanoAcaoViewModel model, int id)
        {
            //var plano = await _context.planoAcoes.FindAsync(id);

            //if (plano == null)
            //{
            //    return null;
            //}
            //var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            //var colaborador = await _context.Colaboradores.FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);

            //var responsavel = await _context.ResponsavelTratativas.FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);

            //if (responsavel == null)
            //{
            //    return null;
            //}

            //if (plano.IdColaborador != responsavel.IdUsuario)
            //{
            //    return null;
            //}
            //if (responsavel == null)
            //{
            //    return null;
            //}

            //if (plano.IdColaborador != responsavel.IdUsuario)
            //{
            //    return null;
            //}

            //if (model.Status == EStatus.EmAndamento && plano.Status == EStatus.Aberto)
            //{
            //    // O responsável pode alterar o status de Aberto para Em Andamento
            //    plano.AtualizaStatus(model);
            //}
            //else if (model.Status == EStatus.AguardandoAprovacao && plano.Status == EStatus.Aberto)
            //{
            //    // O responsável pode solicitar a aprovação, alterando o status para Aguardando Aprovação
            //    plano.AtualizaStatus(model);
            //}
            //else if ((model.Status == EStatus.Concluído || model.Status == EStatus.Reprovado) && plano.Colaborador == colaborador)
            //{
            //    // Somente o colaborador aprovador pode alterar o status para Aprovado ou Reprovado
            //    plano.AtualizaStatus(model);
            //}
            //else
            //{
            //    return null; 
            //}

            //await _context.SaveChangesAsync();

            //return plano;
        }
    }
}
