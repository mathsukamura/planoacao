using apiplanoacao.Data;
using apiplanoacao.Models;
using apiplanoacao.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiplanoacao.Services.CadastroUsuario
{
    public interface ICadastroUsuarioService
    {
        Task<UsuarioModel> PostAsync(UsuarioViewModel model);

        Task<IList<UsuarioModel>> GetAllAsync();
    }
    public class CadastrarUsuarioService : ICadastroUsuarioService
    {
        private readonly ContextDb _context;

        public CadastrarUsuarioService(ContextDb context) => _context = context;

        public async Task<UsuarioModel> PostAsync(UsuarioViewModel model)
        {
            var usuario = model.CriarUsuario();

            await _context.AddAsync(usuario);

            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<IList<UsuarioModel>> GetAllAsync()
        {
            var usuario = await _context.Usuarios.AsNoTracking().ToListAsync();

            return usuario;
        }

    }
}
