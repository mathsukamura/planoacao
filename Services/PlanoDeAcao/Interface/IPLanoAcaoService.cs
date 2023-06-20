using apiplanoacao.Models;
using apiplanoacao.Viewmodels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiplanoacao.Services.PlanoDeAcao.Interface
{
    public interface IPlanoAcaoService
    {
        public Task<IList<PlanoAcaoModel>> GetAsync();

        public Task<PlanoAcaoModel> PostAsync(PlanoAcaoViewModel model);

        public Task<PlanoAcaoModel> PutAsync(PlanoAcaoViewModel model, int id);

        public Task<bool> DeleteAsync(int id);

        public void AlterarStatus(PlanoAcaoViewModel model, int id);

    }
}
