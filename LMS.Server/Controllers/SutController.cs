using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Server.Data; 
using LMS.Shared;

namespace LMS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SutController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SutController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(SutKayitDto dto)
        {
        
            var yeniKayit = new SutKayit 
            { 
                HayvanID = dto.HayvanID, 
                MiktarLitre = dto.MiktarLitre, 
                BirimFiyat = dto.BirimFiyat, 
                KayitTarihi = dto.KayitTarihi 
            };
            _context.SutKayitlari.Add(yeniKayit);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("aylik-rapor")]
        public async Task<IActionResult> GetAylikRapor()
        {
           
            var rapor = await _context.GunlukSutOzetleri
                .FromSqlRaw("EXEC sp_AylikSutRaporu")
                .ToListAsync();
            return Ok(rapor);
        }
    }
}