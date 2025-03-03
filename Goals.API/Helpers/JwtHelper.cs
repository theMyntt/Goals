using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Goals.API.Abstractions.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace Goals.API.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private readonly SigningCredentials _creds;
        private readonly IConfigurationSection _jwt;

        public JwtHelper(IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("Jwt");
            _jwt = jwtSection;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["SecretKey"]!));
            _creds = new(key, SecurityAlgorithms.HmacSha256);
        }

        public string GenerateToken(string name, string email)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
            };

            var token = new JwtSecurityToken(
                issuer: _jwt["Issuer"]!,
                audience: _jwt["Audience"]!,
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: _creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
