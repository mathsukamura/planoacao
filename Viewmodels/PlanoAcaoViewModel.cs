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
        public int Id { get; set; }
        public int ColaboradorAprovador { get; set; }

        public ICollection<UsuarioModel> ResponsaveisTratativa { get; set; }

        public List<string> NomesResponsaveisTratativa => ResponsaveisTratativa?.Select(u => u.Nome).ToList();

        public string DescricaoAcao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public EStatus Status { get; set; }

        public PlanoAcaoModel CreatePlano()
        {
            return new PlanoAcaoModel
            {
                DescricaoAcao = DescricaoAcao,
                ResponsaveisTratativa = (ICollection<UsuarioModel>)NomesResponsaveisTratativa, 
                DataInicio = DataInicio,
                DataFim = DataFim,
                Status = Status
            };
        }
    }
}
