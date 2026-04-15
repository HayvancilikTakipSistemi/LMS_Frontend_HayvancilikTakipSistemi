using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Server.Repositories;
using LMS.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StaffController : ControllerBase
    {
        private readonly IGenericRepository<Staff> _staffRepository;

        public StaffController(IGenericRepository<Staff> staffRepository)
        {
            _staffRepository = staffRepository;
        }

        // GET: api/staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaff()
        {
            var staff = await _staffRepository.GetAllAsync();
            return Ok(staff);
        }

        // GET: api/staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        // POST: api/staff
        [HttpPost]
        public async Task<ActionResult<Staff>> CreateStaff([FromBody] Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _staffRepository.AddAsync(staff);
            await _staffRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStaffById), new { id = staff.StaffID }, staff);
        }

        // PUT: api/staff/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] Staff staff)
        {
            if (id != staff.StaffID)
            {
                return BadRequest();
            }

            var existingStaff = await _staffRepository.GetByIdAsync(id);
            if (existingStaff == null)
            {
                return NotFound();
            }

            // Özellikleri güncelle
            existingStaff.FullName = staff.FullName;
            existingStaff.Role = staff.Role;
            existingStaff.Salary = staff.Salary;
            existingStaff.HireDate = staff.HireDate;
            existingStaff.Phone = staff.Phone;

            _staffRepository.Update(existingStaff);
            await _staffRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/staff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _staffRepository.Remove(staff);
            await _staffRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
