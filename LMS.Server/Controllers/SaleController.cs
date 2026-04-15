using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Server.Data;
using LMS.Shared.DTOs;
using LMS.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SaleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SaleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/sale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetAllSales()
        {
            var sales = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Farmer)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
                
            return Ok(sales);
        }

        // GET: api/sale/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSaleById(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Farmer)
                .Include(s => s.SaleDetails)
                    .ThenInclude(sd => sd.ProductType)
                .Include(s => s.SaleDetails)
                    .ThenInclude(sd => sd.Animal)
                .FirstOrDefaultAsync(s => s.SaleID == id);

            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        // GET: api/sale/income-report
        [HttpGet("income-report")]
        public async Task<ActionResult<IEnumerable<CiftciGelirRaporuDto>>> GetIncomeReport()
        {
            var report = await _context.CiftciGelirRaporlari
                .FromSqlRaw("EXEC sp_CiftciGelirRaporu")
                .ToListAsync();

            return Ok(report);
        }

        // POST: api/sale
        [HttpPost]
        public async Task<ActionResult<Sale>> CreateSale([FromBody] Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSaleById), new { id = sale.SaleID }, sale);
        }
    }
}
