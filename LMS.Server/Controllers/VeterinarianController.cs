using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Server.Repositories;
using LMS.Shared.DTOs;
using LMS.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VeterinarianController : ControllerBase
    {
        private readonly IGenericRepository<Veterinarian> _veterinarianRepository;

        public VeterinarianController(IGenericRepository<Veterinarian> veterinarianRepository)
        {
            _veterinarianRepository = veterinarianRepository;
        }

        // GET: api/veterinarian
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeterinarianDto>>> GetAllVeterinarians()
        {
            var veterinarians = await _veterinarianRepository.GetAllAsync();

            var veterinarianDtos = veterinarians.Select(v => new VeterinarianDto
            {
                VeterinarianID = v.VeterinarianID,
                FirstName = v.FirstName,
                LastName = v.LastName,
                Phone = v.PhoneNumber,
                Specialization = v.Specialization,
                Address = v.Address,
                Email = v.Email,
                RegistrationDate = v.CreatedDate
            });

            return Ok(veterinarianDtos);
        }

        // GET: api/veterinarian/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VeterinarianDto>> GetVeterinarianById(int id)
        {
            var veterinarian = await _veterinarianRepository.GetByIdAsync(id);
            if (veterinarian == null)
                return NotFound();

            var veterinarianDto = new VeterinarianDto
            {
                VeterinarianID = veterinarian.VeterinarianID,
                FirstName = veterinarian.FirstName,
                LastName = veterinarian.LastName,
                Phone = veterinarian.PhoneNumber,
                Specialization = veterinarian.Specialization,
                Address = veterinarian.Address,
                Email = veterinarian.Email,
                RegistrationDate = veterinarian.CreatedDate
            };

            return Ok(veterinarianDto);
        }

        // POST: api/veterinarian
        [HttpPost]
        public async Task<ActionResult<VeterinarianDto>> CreateVeterinarian([FromBody] VeterinarianDto veterinarianDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var veterinarian = new Veterinarian
            {
                FirstName = veterinarianDto.FirstName,
                LastName = veterinarianDto.LastName,
                PhoneNumber = veterinarianDto.Phone,
                Specialization = veterinarianDto.Specialization,
                Address = veterinarianDto.Address,
                Email = veterinarianDto.Email,
                CreatedDate = DateTime.Now
            };

            await _veterinarianRepository.AddAsync(veterinarian);
            await _veterinarianRepository.SaveChangesAsync();

            veterinarianDto.VeterinarianID = veterinarian.VeterinarianID;
            veterinarianDto.RegistrationDate = veterinarian.CreatedDate;

            return CreatedAtAction(nameof(GetVeterinarianById), new { id = veterinarian.VeterinarianID }, veterinarianDto);
        }

        // PUT: api/veterinarian/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVeterinarian(int id, [FromBody] VeterinarianDto veterinarianDto)
        {
            if (id != veterinarianDto.VeterinarianID)
                return BadRequest();

            var veterinarian = await _veterinarianRepository.GetByIdAsync(id);
            if (veterinarian == null)
                return NotFound();

            veterinarian.FirstName = veterinarianDto.FirstName;
            veterinarian.LastName = veterinarianDto.LastName;
            veterinarian.PhoneNumber = veterinarianDto.Phone;
            veterinarian.Specialization = veterinarianDto.Specialization;
            veterinarian.Address = veterinarianDto.Address;
            veterinarian.Email = veterinarianDto.Email;
            veterinarian.UpdatedDate = DateTime.Now;

            _veterinarianRepository.Update(veterinarian);
            await _veterinarianRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/veterinarian/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeterinarian(int id)
        {
            var veterinarian = await _veterinarianRepository.GetByIdAsync(id);
            if (veterinarian == null)
                return NotFound();

            _veterinarianRepository.Remove(veterinarian);
            await _veterinarianRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
