using apiplanoacao.Models;
using System;

namespace apiplanoacao.Viewmodels
{
    public class PlanoAcaoViewModel
    {
        public int IdColaborador { get; set; }

        public string DescricaoAcao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public EStatus Status { get; set; }

        public PlanoAcaoModel CreatePlano()
        {
            return new PlanoAcaoModel
            {
                DescricaoAcao = DescricaoAcao,
                DataInicio = DataInicio,
                DataFim = DataFim,
                Status = Status
            };
        }
    }
}
