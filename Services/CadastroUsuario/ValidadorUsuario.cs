using apiplanoacao.Viewmodels;
using FluentValidation;

namespace apiplanoacao.Services.CadastroUsuario
{
    public class ValidadorUsuario : AbstractValidator<UsuarioViewModel>
    {
        public ValidadorUsuario()
        {
            RuleFor(usuario => usuario.Nome)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo nome é obrigatório");

            RuleFor(usuario => usuario.Nome)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(1, 100)
                .WithMessage("O nome do usuário deve ter no máximo 100 caracteres.");

            RuleFor(usuario => usuario.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("E-mail não pode ser nulo");

            RuleFor(usuario => usuario.Senha).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("Senha não pode ser nula");


        }
    }
}
