using apiplanoacao.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace apiplanoacao.Services.GerarToken
{
    public interface ITokenService
    {
           string GerarToken(UsuarioModel usuarios);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(UsuarioModel usuarios)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var issuer = _config.GetValue<dynamic>("Jwt:Issuer");
            var audience = _config.GetValue<dynamic>("Jwt:Audience");
            var keyJwt = _config.GetValue<dynamic>("Jwt:Key");

            var key = Encoding.ASCII.GetBytes(keyJwt);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sid, usuarios.Id.ToString()),    
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(4),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
