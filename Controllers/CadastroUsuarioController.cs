using apiplanoacao.Services.CadastroUsuario;
using apiplanoacao.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace apiplanoacao.Controllers
{
    [Authorize]
    [Route("cadastro")]
    public class CadastroUsuarioController : ControllerBase
    {
        private readonly ICadastroUsuarioService _cadastroUsuario;

        public CadastroUsuarioController(ICadastroUsuarioService cadastroUsuario)
        {
            _cadastroUsuario = cadastroUsuario;
        }

        [HttpPost]
        public async Task<IActionResult> Postasync(UsuarioViewModel Model)
        {
            var usuario = await _cadastroUsuario.PostAsync(Model);

            return Created($"v1/usuario/{usuario.Id}", new { usuario.Id });
        }

        [HttpGet("Cadastros")]
        public async Task<ActionResult> GetAsync()
        {
            var usuario = await _cadastroUsuario.GetAsync();

            return Ok(usuario);
        }
    }
}
