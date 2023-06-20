using apiplanoacao.Services.PlanoDeAcao;
using apiplanoacao.Viewmodels;
using System;
using System.Collections.Generic;

namespace apiplanoacao.Models
{
    public enum EStatus
    {
        Aberto = 0, 
        EmAndamento = 1,
        AguardandoAprovacao = 2,
        Reprovado = 3,
        Concluído = 4
    }

    public class PlanoAcaoModel
    {
        public int Id { get; set; }
        
        public int IdUsuario { get; set; }

        public int IdColaboradorAprovador { get; set; }

        public int ResponsavelTratativa { get; set; }

        public string DescricaoAcao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public EStatus Status { get; set; }

        public ICollection<UsuarioModel> ResponsaveisTratativa { get; set; }

        public UsuarioModel ColaboradorAprovador { get; set; }

        public UsuarioModel Usuario { get; set; }

        public void AtualizaPlano(PlanoAcaoViewModel viewModel)
        {
            if(viewModel == null)
            {
                return;
            }

            DescricaoAcao = viewModel.DescricaoAcao;
            DataInicio = viewModel.DataInicio;
            DataFim = viewModel.DataFim;
            Status = viewModel.Status;
        }

        public void AtualizaStatus(PlanoAcaoViewModel viewModel)
        {
            if(viewModel == null)
            {
                return;
            }

            Status = viewModel.Status;
        }
    }
}
