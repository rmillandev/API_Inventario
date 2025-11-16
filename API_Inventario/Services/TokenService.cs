using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_Inventario.Models;
using API_Inventario.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API_Inventario.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;
        public TokenService(IConfiguration config)
        {
            this.config = config;
        }

        public string GenerarToken(Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);
            var securityKey = new SymmetricSecurityKey(key);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim("UserId", usuario.Id.ToString())
            };

            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
