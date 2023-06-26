using apiplanoacao.Data;
using apiplanoacao.Models;
using apiplanoacao.Services.CadastroUsuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiplanoacao.Viewmodels
{
    public class PlanoAcaoViewModel
    {
        public int IdColaboradorAprovador { get; set; }

        public List<int> ResponsaveisTratativa { get; set; }

        public string DescricaoAcao { get; set; }

        public DateTime DataInicio { get; set; } = DateTime.UtcNow;

        public DateTime DataFim { get; set; } = DateTime.UtcNow;

        public PlanoAcaoModel CreatePlano()
        {
            return new PlanoAcaoModel
            {
                ColaboradorId = IdColaboradorAprovador,
                DescricaoAcao = DescricaoAcao,
                DataInicio = DataInicio,
                DataFim = DataFim        
            };

        }
    }
}
