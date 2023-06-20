using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace apiplanoacao.Services.CapturaUsuario
{
    public interface IObterUsuariorServices
    {
        int ObterUsuarioId();
    }

    public class ObterUsuariorServices : IObterUsuariorServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ObterUsuariorServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int ObterUsuarioId()
        {
            return int.Parse(_httpContextAccessor?.HttpContext?.User!.FindFirstValue(JwtRegisteredClaimNames.Sid));
        }
    }
}
