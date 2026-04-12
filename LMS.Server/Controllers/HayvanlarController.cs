using LMS.Server.Data;
using LMS.Server.Services;
using LMS.Shared.Entities;
using LMS.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class HayvanlarController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly HayvanYapayZekaServisi _aiAnalyzer;

    public HayvanlarController(AppDbContext context, HayvanYapayZekaServisi aiAnalyzer)
    {
        _context = context;
        _aiAnalyzer = aiAnalyzer;
    }

    [HttpGet("{id}/analyze")]
    public async Task<IActionResult> AnalyzeAnimal(int id)
    {
        var animal = await _context.Hayvanlar.FindAsync(id);
        if (animal == null) return NotFound();

        var result = _aiAnalyzer.AnalizEt(animal);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAnimals()
    {
        var role = User.FindFirstValue(ClaimTypes.Role);
        var kullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        IQueryable<Hayvan> query = _context.Hayvanlar;

        // Not: Burada detaylı yetki kurgusu uygulanabilir
        var animals = await query.ToListAsync();
        return Ok(animals);
    }

    [HttpPost]
    public async Task<IActionResult> AddAnimal([FromBody] Hayvan animal)
    {
        var kullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await _context.Kullanicilar.FindAsync(kullaniciId);
        
        if (user == null) return Unauthorized();
        if (user.Rol == Role.Veteriner) return Forbid("Veteriner hayvan ekleyemez.");

        // Eğer kullanıcı bir çiftçi profilini bağlıysa onu kullan
        if (user.BagliCiftciID.HasValue)
        {
            animal.CiftciID = user.BagliCiftciID.Value;
        }
        else
        {
            return BadRequest("Bağlı bir çiftçi profili bulunamadı. Lütfen profilinizi tamamlayın.");
        }

        _context.Hayvanlar.Add(animal);
        await _context.SaveChangesAsync();

        return Created($"/api/hayvanlar/{animal.HayvanID}", animal);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnimal(int id)
    {
        var animal = await _context.Hayvanlar.FindAsync(id);
        if (animal == null) return NotFound();
        return Ok(animal);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        var animal = await _context.Hayvanlar.FindAsync(id);
        if (animal == null) return NotFound();

        _context.Hayvanlar.Remove(animal);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
