using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieRental.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expireMinutes;

        public JwtService(IConfiguration configuration)
        {
            _secret = configuration["JwtSettings:Secret"]!;
            _issuer = configuration["JwtSettings:Issuer"]!;
            _audience = configuration["JwtSettings:Audience"]!;
            _expireMinutes = int.Parse(configuration["JwtSettings:ExpireMinutes"]!);
        }
        public string GenerateToken(Person person)
        {
            List<Claim> claims;
            if (person is Employee employee)
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{employee.FirstName} {employee.LastName}"),
                    new Claim(ClaimTypes.Email, employee.Email),
                    new Claim(ClaimTypes.Role, employee.Role.ToString()) // Dodaj rolę
                };
            }
            else if (person is Client client)
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(ClaimTypes.Role, "Client")
                };
            }
            else
            {
                throw new InvalidOperationException("Unknown person type.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
