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
using System.Collections;

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

        public async Task<IEnumerable<PlanoAcaoModel>> GetAsync()
        {
            var usuario = await ValidaUsuario();

            if (usuario == null)
            {
                return Enumerable.Empty<PlanoAcaoModel>();
            }

            var planoacao = await _context.PlanoAcoes.
                Where(p => p.IdUsuario == usuario.Id)
                .AsNoTracking()
                .ToListAsync();

           
            return (planoacao);

        }

        public async Task<PlanoAcaoModel> GetById(int id)
        {
            var usuario = await ValidaUsuario();

            if (usuario == null)
            {
                return null;
            }

            var plano = await _context.PlanoAcoes
                .Where(p => p.IdUsuario == usuario.Id & p.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (plano== null)
            {
                return null;
            }

            return (plano);
        }

        public async Task<PlanoAcaoModel> PostAsync(PlanoAcaoViewModel model)
        {
            var plano = model.CreatePlano();

            var usuario = await ValidaUsuario();

            if (model?.ResponsaveisTratativa?.Any() ?? false)
            {
                foreach (var responsavelId in model.ResponsaveisTratativa)
                {
                    var responsavel = await _context.Usuarios.FindAsync(responsavelId);

                    if (responsavel != null)
                    {
                        plano.ResponsaveisTratativa.Add(responsavel);
                    }
                }
            }

            plano.IdUsuario = usuario.Id;

            await _context.AddAsync(plano);

            await _context.SaveChangesAsync();

            return plano;
        }


        public async Task<PlanoAcaoModel> PutAsync(PlanoAcaoViewModel model, int id)
        {
            var plano = await _context.PlanoAcoes
                .Include(p => p.ResponsaveisTratativa)
                .FirstOrDefaultAsync(p => p.Id == id);

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

            await AlteraResponsaveisPelaTratativa(plano, model.ResponsaveisTratativa);

            //var responsaveisTratativa = await _context.Usuarios
            //    .Where(u => model.ResponsaveisTratativa.Contains(u.Id))
            //    .ToListAsync();

            //var responsaveisRemover = plano.ResponsaveisTratativa
            //    .Where(r => !responsaveisTratativa.Contains(r))
            //    .ToList();

            //foreach (var responsavelRemover in responsaveisRemover)
            //{
            //    plano.ResponsaveisTratativa.Remove(responsavelRemover);
            //}

            //var responsaveisAdicionar = responsaveisTratativa
            //    .Where(r => !plano.ResponsaveisTratativa.Contains(r))
            //    .ToList();

            //foreach (var responsavelAdicionar in responsaveisAdicionar)
            //{
            //    plano.ResponsaveisTratativa.Add(responsavelAdicionar);
            //}

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

        private async Task AlteraResponsaveisPelaTratativa(PlanoAcaoModel plano, List<int> responsaveisTratativa)
        {
            var novosResponsaveis = responsaveisTratativa.Where(id => !plano.ResponsaveisTratativa.Any(db => db.Id == id)).ToList();

            foreach (var idResponsavel in novosResponsaveis)
            {
                var colaborador = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == idResponsavel);

                if (colaborador != null)
                {
                    plano.ResponsaveisTratativa.Add(colaborador);
                }
            }

            var responsaveisRemover = plano.ResponsaveisTratativa
                .Where(r => !responsaveisTratativa.Contains(r.Id))
                .ToList();

            foreach (var responsavelRemover in responsaveisRemover)
            {
                plano.ResponsaveisTratativa.Remove(responsavelRemover);
            }
        }
    }
}
