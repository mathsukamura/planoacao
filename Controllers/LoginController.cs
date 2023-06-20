using apiplanoacao.Models;
using apiplanoacao.Services.Acesso;
using apiplanoacao.Services.GerarToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace apiplanoacao.Controllers
{
    [Authorize]
    [Route("login")]
    [AllowAnonymous]
    public class LoginController :ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;

        public LoginController(ILoginService loginService, ITokenService tokenService)
        {
            _loginService = loginService;
            _tokenService = tokenService;
        }
        [HttpPost]
        public async Task<ActionResult> AutenticacaoAsync(Login logins)
        {
            var usuario = await _loginService.AutenticacaoAsync(logins);

            if (usuario == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            var token = _tokenService.GerarToken(usuario);

            return Ok(new { token });
        }
    }
}
