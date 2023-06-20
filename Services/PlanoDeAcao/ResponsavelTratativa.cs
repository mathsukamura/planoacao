using apiplanoacao.Data;
using apiplanoacao.Models;
using apiplanoacao.Services.CapturaUsuario;
using apiplanoacao.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiplanoacao.Services.PlanoDeAcao
{
    public class ResponsavelTratativa
    {
        private readonly ContextDb _context;

        private readonly IObterUsuariorServices _obterUsuariorServices;
        public ResponsavelTratativa(ContextDb context, IObterUsuariorServices obterUsuariorServices )
        {
            _context = context;
            _obterUsuariorServices = obterUsuariorServices;
        }


        //public async Task<IList<PlanoAcaoModel>> GetAsync()
        //{
        //    var idUsuario = _obterUsuariorServices.ObterUsuarioId();

        //    var responsavel = await _context.ResponsavelTratativas.FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);

        //    if (responsavel == null)
        //    {
        //        return null;
        //    }

        //    var planoacao = await _context.planoAcoes
        //        .Where(p => p.Colaborador.Id == responsavel.Id)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    if (planoacao == null)
        //    {
        //        return null;
        //    }

        //    return (planoacao);
        //}

        //public async Task<PlanoAcaoModel> AlterarStatus(PlanoAcaoViewModel model, int id)
        //{
        //    var plano = await _context.planoAcoes.FindAsync(id);

        //    if (plano == null)
        //    {
        //        return null;
        //    }
        //    var idUsuario = _obterUsuariorServices.ObterUsuarioId();

        //    var colaborador = await _context.Colaboradores.FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);

        //    var responsavel = await _context.ResponsavelTratativas.FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);

        //    if (responsavel == null)
        //    {
        //        return null;
        //    }

        //    if (plano.IdColaborador != responsavel.IdUsuario)
        //    {
        //        return null;
        //    }
        //    if (responsavel == null)
        //    {
        //        return null;
        //    }

        //    if (plano.IdColaborador != responsavel.IdUsuario)
        //    {
        //        return null;
        //    }

        //    if (model.Status == EStatus.EmAndamento && plano.Status == EStatus.Aberto)
        //    {
        //        O responsável pode alterar o status de Aberto para Em Andamento
        //        plano.AtualizaStatus(model);
        //    }
        //    else if (model.Status == EStatus.AguardandoAprovacao && plano.Status == EStatus.Aberto)
        //    {
        //        O responsável pode solicitar a aprovação, alterando o status para Aguardando Aprovação
        //        plano.AtualizaStatus(model);
        //    }
        //    else if ((model.Status == EStatus.Concluído || model.Status == EStatus.Reprovado) && plano.Colaborador == colaborador)
        //    {
        //        Somente o colaborador aprovador pode alterar o status para Aprovado ou Reprovado
        //        plano.AtualizaStatus(model);
        //    }
        //    else
        //    {
        //        return null; // Status inválido ou permissões insuficientes
        //    }

        //    await _context.SaveChangesAsync();

        //    return plano;
        //}
    }
}
