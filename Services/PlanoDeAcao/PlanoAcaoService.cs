using apiplanoacao.Data;
using apiplanoacao.Models;
using apiplanoacao.Services.CapturaUsuario;
using apiplanoacao.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using apiplanoacao.Services.PlanoDeAcao.Interface;
using apiplanoacao.Interfaces;

namespace apiplanoacao.Services.PlanoDeAcao
{
    public class PlanoAcaoService : IPlanoAcaoService
    {
        private readonly IPlanoAcaoRepository _planoAcaoRepository;
        private readonly IObterUsuariorServices _obterUsuariorServices;

        public PlanoAcaoService(
            IObterUsuariorServices obterUsuariorServices, 
            IPlanoAcaoRepository planoAcaoRepository) 
        {
            _obterUsuariorServices = obterUsuariorServices;
            _planoAcaoRepository = planoAcaoRepository;
        }

        public async Task<IEnumerable<PlanoAcaoModel>> GetAsync()
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var planoacao = await _planoAcaoRepository.ObterPlanosPorUsuarioAsync(idUsuario);
           
            return (planoacao);
        }

        public async Task<PlanoAcaoModel> GetById(int id)
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var plano = await _planoAcaoRepository.GetAsync(id);

            if (plano== null || plano.IdUsuario != idUsuario)
            {
                return null;
            }

            return (plano);
        }

        public async Task<PlanoAcaoModel> PostAsync(PlanoAcaoViewModel model)
        {
            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            var plano = model.CreatePlano(idUsuario);

            await AlteraResponsaveisPelaTratativa(plano, model.ResponsaveisTratativa);
              
            await _planoAcaoRepository.AddAsync(plano);

            await _planoAcaoRepository.CommitAsync();

            return plano;
        }


        public async Task<PlanoAcaoModel> PutAsync(PlanoAcaoViewModel model, int id)
        {
            var plano = await _planoAcaoRepository.ObterComDetalhesAsync(id);

            if (plano == null)
            {
                return null;
            }

            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            if (plano.IdUsuario != idUsuario)
            {
                return null;
            }

            plano.AtualizaPlano(model);

            await AlteraResponsaveisPelaTratativa(plano, model.ResponsaveisTratativa);

            await _planoAcaoRepository.CommitAsync();

            return plano;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plano = await _planoAcaoRepository.GetAsync(id);

            if (plano == null)
            {
                return false;
            }

            var idUsuario = _obterUsuariorServices.ObterUsuarioId();

            if (plano.IdUsuario != idUsuario)
            {
                return false;
            }

            await _planoAcaoRepository.DeteleAsync(plano);

            await _planoAcaoRepository.CommitAsync();

            return true;
        }

        private async Task AlteraResponsaveisPelaTratativa(PlanoAcaoModel plano, List<int> responsaveisTratativa)
        {
            if (!responsaveisTratativa?.Any() ?? false)
            {
                return;
            }

            var novosResponsaveis = responsaveisTratativa.Where(id => !plano.ResponsaveisTratativa.Any(db => db.Id == id)).ToList();

            foreach (var idResponsavel in novosResponsaveis)
            {
                var colaborador = await _planoAcaoRepository.ObterColaboradorAsync(idResponsavel);

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
