using LMS.Server.Repositories;
using LMS.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BarnController : ControllerBase
    {
        private readonly IGenericRepository<Barn> _barnRepository;

        public BarnController(IGenericRepository<Barn> barnRepository)
        {
            _barnRepository = barnRepository;
        }

        // GET: api/barn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Barn>>> GetAllBarns()
        {
            var barns = await _barnRepository.GetAllAsync();
            return Ok(barns);
        }

        // GET: api/barn/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Barn>> GetBarnById(int id)
        {
            var barn = await _barnRepository.GetByIdAsync(id);
            if (barn == null)
                return NotFound();

            return Ok(barn);
        }

        // POST: api/barn
        [HttpPost]
        public async Task<ActionResult<Barn>> CreateBarn([FromBody] Barn barn)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _barnRepository.AddAsync(barn);
            await _barnRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBarnById), new { id = barn.BarnID }, barn);
        }

        // PUT: api/barn/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBarn(int id, [FromBody] Barn barn)
        {
            if (id != barn.BarnID)
                return BadRequest();

            var existingBarn = await _barnRepository.GetByIdAsync(id);
            if (existingBarn == null)
                return NotFound();

            existingBarn.BarnName = barn.BarnName;
            existingBarn.Capacity = barn.Capacity;
            existingBarn.Address = barn.Address;

            _barnRepository.Update(existingBarn);
            await _barnRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/barn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarn(int id)
        {
            var barn = await _barnRepository.GetByIdAsync(id);
            if (barn == null)
                return NotFound();

            _barnRepository.Remove(barn);
            await _barnRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
