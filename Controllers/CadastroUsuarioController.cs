using apiplanoacao.Models;
using apiplanoacao.Services.CadastroUsuario;
using apiplanoacao.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiplanoacao.Controllers
{
    [Route("cadastro")]
    public class CadastroUsuarioController : ControllerBase
    {
        private readonly ICadastroUsuarioService _cadastroUsuario;

        public CadastroUsuarioController(ICadastroUsuarioService cadastroUsuario)
        {
            _cadastroUsuario = cadastroUsuario;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(UsuarioViewModel model)
        {
            ValidadorUsuario validador = new ValidadorUsuario();

            var result = validador.Validate(model);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var usuario = await _cadastroUsuario.PostAsync(model);

            return Created($"v1/usuario/{usuario.Id}", new { usuario.Id });
        }

        [HttpGet("Cadastros")]
        public async Task<ActionResult> GetAllAsync()
        {
            var usuario = await _cadastroUsuario.GetAllAsync();

            return Ok(usuario);
        }
    }
}
