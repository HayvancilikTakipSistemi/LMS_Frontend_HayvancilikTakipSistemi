using LMS.Server.Data;
using LMS.Shared.DTOs.Auth;
using LMS.Shared.Entities;
using LMS.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LMS.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (await _context.Kullanicilar.AnyAsync(u => u.KullaniciAdi == dto.Username))
            return BadRequest("Bu kullanıcı adı zaten alınmış.");

        var user = new Kullanici
        {
            KullaniciAdi = dto.Username,
            SifreHash = HashPassword(dto.Password),
            Rol = (Role)dto.RoleId
        };

        _context.Kullanicilar.Add(user);
        await _context.SaveChangesAsync();

        // Eğer rol Çiftçi ise otomatik bir Çiftçi profili oluştur ve bağla
        if (user.Rol == Role.Ciftci)
        {
            var ciftci = new Ciftci
            {
                Ad = user.KullaniciAdi, // Varsayılan olarak kullanıcı adını ad olarak kullan
                Soyad = "Kullanıcısı",
                KayitTarihi = DateTime.Now
            };
            _context.Ciftciler.Add(ciftci);
            await _context.SaveChangesAsync();

            user.BagliCiftciID = ciftci.CiftciID;
            await _context.SaveChangesAsync();
        }

        return Ok("Kayıt başarılı.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var passwordHash = HashPassword(dto.Password);
        var user = await _context.Kullanicilar.FirstOrDefaultAsync(u => u.KullaniciAdi == dto.Username && u.SifreHash == passwordHash);

        if (user == null || !user.AktifMi)
            return Unauthorized("Geçersiz kullanıcı adı veya şifre.");

        var token = GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private string GenerateJwtToken(Kullanici user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "FallbackSecretKey");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.KullaniciID.ToString()),
            new Claim(ClaimTypes.Name, user.KullaniciAdi),
            new Claim(ClaimTypes.Role, user.Rol.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
