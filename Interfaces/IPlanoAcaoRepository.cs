using apiplanoacao.Data;
using apiplanoacao.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace apiplanoacao.Interfaces
{
    public interface IPlanoAcaoRepository
    {
        Task<PlanoAcaoModel> GetAsync(int id);
        Task DeteleAsync(PlanoAcaoModel model);
        Task<int> CommitAsync();
        Task<IEnumerable<PlanoAcaoModel>> ObterPlanosPorUsuarioAsync(int idUsuario);
        Task AddAsync(PlanoAcaoModel plano);
        Task<PlanoAcaoModel> ObterComDetalhesAsync(int id);
        Task<UsuarioModel> ObterColaboradorAsync(int idResponsavel);
    }

    public class PlanoAcaoRepository : IPlanoAcaoRepository
    {
        private readonly ContextDb _context;

        public PlanoAcaoRepository(ContextDb context)
        {
            _context = context;
        }

        public async Task<PlanoAcaoModel> GetAsync(int id)
        {
            var plano = await _context.PlanoAcoes.FirstOrDefaultAsync(x => x.Id == id);
            return plano;
        }

        public async Task DeteleAsync(PlanoAcaoModel model)
        {
            _context.PlanoAcoes.Remove(model);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlanoAcaoModel>> ObterPlanosPorUsuarioAsync(int idUsuario)
        {
           return await _context.PlanoAcoes.
                Where(p => p.IdUsuario == idUsuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(PlanoAcaoModel plano)
        {
            await _context.AddAsync(plano);
        }

        public async Task<PlanoAcaoModel> ObterComDetalhesAsync(int id)
        {
            return await _context.PlanoAcoes
                .Include(p => p.ResponsaveisTratativa)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<UsuarioModel> ObterColaboradorAsync(int idResponsavel)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == idResponsavel);
        }
    }
}
