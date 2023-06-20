using apiplanoacao.Services.Acesso;
using apiplanoacao.Services.CadastroUsuario;
using apiplanoacao.Services.CapturaUsuario;
using apiplanoacao.Services.GerarToken;
using apiplanoacao.Services.PlanoDeAcao;
using apiplanoacao.Services.PlanoDeAcao.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace apiplanoacao
{
    public class DependencyInjection
    {
        public static void ConfigureInterfaces(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICadastroUsuarioService, CadastrarUsuarioService>();
            services.AddScoped<IPlanoAcaoService, PlanoAcaoService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IObterUsuariorServices, ObterUsuariorServices>();
        }    
    }
}
