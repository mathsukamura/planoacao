using apiplanoacao.Data;
using apiplanoacao.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace apiplanoacao.Services.Acesso
{
    public interface ILoginService
    {
        Task<UsuarioModel> AutenticacaoAsync(Login logins);
    }
    public class LoginService : ILoginService
    {
        private readonly ContextDb _context;

        public LoginService(ContextDb context) => _context = context;

        public async Task<UsuarioModel> AutenticacaoAsync(Login login)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Email == login.Email
                && x.Senha == login.Senha);

            return usuario;
        }
    }
}
