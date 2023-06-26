using apiplanoacao.Models;
using apiplanoacao.Viewmodels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiplanoacao.Services.PlanoDeAcao.Interface
{
    public interface IPlanoAcaoService
    {
        public Task<IList<PlanoAcaoModel>> GetAsync();

        public Task<PlanoAcaoModel> GetById(int id);

        public Task<PlanoAcaoModel> PostAsync(PlanoAcaoViewModel model);

        public Task<PlanoAcaoModel> PutAsync(PlanoAcaoViewModel model, int id);

        public Task<bool> DeleteAsync(int id);
    }
    public interface IPlanoAcaoRegrasService
    {
        public Task<IList<PlanoAcaoModel>> GetTratativasPentendesAsync(EStatus? status, DateTime? dataInicio, DateTime? dataFim);

        public Task<IList<PlanoAcaoModel>> GetAprovacoesPendentesAsync(EStatus? status, DateTime? dataInicio, DateTime? dataFim);

        public Task<bool> AlterarStatusPlanoAcaoCompleto(List<int> ids, EStatus novoStatus);

    }
}
