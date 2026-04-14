using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Server.Data;
using LMS.Shared;

namespace LMS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public YemController(ApplicationDbContext context) => _context = context;

 ß
        [HttpPost("yem-ver")]
        public async Task<IActionResult> YemVer(YemKayitDto dto)
        {
            
            var stoktakiYem = await _context.Yemler.FindAsync(dto.YemID);
            
            if (stoktakiYem == null) return NotFound("Böyle bir yem türü yok!");

            
            if (stoktakiYem.StokMiktar < dto.KullanilanMiktar)
            {
                return BadRequest("Yetersiz stok! Mevcut stok: " + stoktakiYem.StokMiktar);
            }

        
            stoktakiYem.StokMiktar -= dto.KullanilanMiktar;

            _context.YemKayitlari.Add(new YemKayit {
                YemID = dto.YemID,
                HayvanID = dto.HayvanID,
                KullanilanMiktar = dto.KullanilanMiktar,
                KayitTarihi = DateTime.Now
            });

            await _context.SaveChangesAsync();
            return Ok("Yem başarıyla verildi, stok güncellendi.");
        }
    }
}