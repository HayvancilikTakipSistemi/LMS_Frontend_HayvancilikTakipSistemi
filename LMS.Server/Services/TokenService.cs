using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LMS.Server.Models;
using Microsoft.IdentityModel.Tokens;

namespace LMS.Server.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(AppUser user, IEnumerable<string> roles);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private const int ExpirationMinutes = 60;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(AppUser user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(
                _configuration["Jwt:SecretKey"] ?? "SuperSecretKeyThatIsAtLeast32CharactersLong!");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim("FullName", user.FullName ?? user.UserName ?? ""),
                new Claim("FarmerId", (user.FarmerId ?? 0).ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(ExpirationMinutes),
                Issuer = _configuration["Jwt:Issuer"] ?? "LmsServer",
                Audience = _configuration["Jwt:Audience"] ?? "LmsClient",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
