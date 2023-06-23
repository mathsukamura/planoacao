using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using apiplanoacao.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Globalization;

namespace apiplanoacao.Infra
{
    public class MiddlewareAuthentication
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public MiddlewareAuthentication(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var issuer = _config.GetValue<dynamic>("Jwt:Issuer");
            var audience = _config.GetValue<dynamic>("Jwt:Audience");
            var keyJwt = _config.GetValue<dynamic>("Jwt:Key");

            var key = Encoding.ASCII.GetBytes(keyJwt);

            try
            {
                var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(token))
                {
                    await _next(httpContext);
                    return;
                }

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                var claims = claimsPrincipal.Claims.ToList();

                httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "custom"));

                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }

    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                if (httpContext.Request.Path.Value.EndsWith("/login") ||
                    httpContext.Request.Path.Value.EndsWith("swagger/index.html") || httpContext.Request.Path.Value.EndsWith("/cadastro"))
                {
                    await _next(httpContext);

                    return;
                }

                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }

                var context = (ContextDb)httpContext.RequestServices.GetService(typeof(ContextDb));

                var claim = httpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sid);

                var idUsuario = claim.Value;

                int id = int.Parse(idUsuario);

                var acessoPermitido = await context.Usuarios
                      .AnyAsync(u => u.Id == id);

                if (acessoPermitido == true)
                {
                    await _next(httpContext);

                    return;
                }

            }
            catch (Exception ex)
            {

                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
        }
    }
}
