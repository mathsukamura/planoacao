using apiplanoacao.Interfaces;
using apiplanoacao.Services.Acesso;
using apiplanoacao.Services.CadastroUsuario;
using apiplanoacao.Services.CapturaUsuario;
using apiplanoacao.Services.GerarToken;
using apiplanoacao.Services.Notification;
using apiplanoacao.Services.PlanoDeAcao;
using apiplanoacao.Services.PlanoDeAcao.Interface;
using apiplanoacao.Services.tratativas;
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
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ICadastroUsuarioService, CadastrarUsuarioService>();
            services.AddScoped<IPlanoAcaoRepository, PlanoAcaoRepository>();
            services.AddScoped<IPlanoAcaoService, PlanoAcaoService>();
            services.AddScoped<IPlanoAcaoRegrasService, PlanosAcaoRegrasService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IObterUsuariorServices, ObterUsuariorServices>();
            services.AddScoped<ITratativaService, TratativasServices>();
        }    
    }
}
