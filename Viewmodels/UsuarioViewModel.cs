using apiplanoacao.Models;

namespace apiplanoacao.Viewmodels
{
    public class UsuarioViewModel
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public UsuarioModel CriarUsuario()
        {
            return new UsuarioModel
            {
                Nome = Nome,
                Email = Email,
                Senha = Senha
            };
        }

    }
}
