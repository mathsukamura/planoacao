using apiplanoacao.Services.PlanoDeAcao;
using apiplanoacao.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public PlanoAcaoModel()
        {
            Status = EStatus.Aberto;
            ResponsaveisTratativa = new List<UsuarioModel>();
        }

        public int Id { get; set; }
        
        public int IdUsuario { get; set; }

        public int ColaboradorId { get; set; }

        public string DescricaoAcao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public EStatus Status { get;  set; }

        public ICollection<UsuarioModel> ResponsaveisTratativa { get; private set; }

        public UsuarioModel ColaboradorAprovador { get; set; }

        public UsuarioModel Usuario { get; set; }

        public void AtualizaPlano(PlanoAcaoViewModel viewModel)
        {
            if(viewModel == null)
            {
                return;
            }
            ColaboradorId = viewModel.IdColaboradorAprovador;
            DescricaoAcao = viewModel.DescricaoAcao;
            DataInicio = viewModel.DataInicio;
            DataFim = viewModel.DataFim;
        }

        public bool StatusAtualExigeColaboradorResponsavel(int idUsuario)
        {
            if (Status == EStatus.Aberto || Status == EStatus.EmAndamento)
            {
                if (ResponsaveisTratativa.Any(x => x.Id == idUsuario))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
