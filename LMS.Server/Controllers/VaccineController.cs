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
    public class VaccineController : ControllerBase
    {
        private readonly IGenericRepository<Vaccine> _vaccineRepository;

        public VaccineController(IGenericRepository<Vaccine> vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaccineDto>>> GetAllVaccines()
        {
            var vaccines = await _vaccineRepository.GetAllAsync();
            var vaccineDtos = vaccines.Select(v => new VaccineDto
            {
                VaccineID = v.VaccineID,
                VaccineName = v.VaccineName,
                PeriodDays = v.PeriodDays
            });
            return Ok(vaccineDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VaccineDto>> GetVaccineById(int id)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(id);
            if (vaccine == null) return NotFound();

            var vaccineDto = new VaccineDto
            {
                VaccineID = vaccine.VaccineID,
                VaccineName = vaccine.VaccineName,
                PeriodDays = vaccine.PeriodDays
            };
            return Ok(vaccineDto);
        }

        [HttpPost]
        public async Task<ActionResult<VaccineDto>> CreateVaccine([FromBody] VaccineDto vaccineDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vaccine = new Vaccine
            {
                VaccineName = vaccineDto.VaccineName,
                PeriodDays = vaccineDto.PeriodDays
            };

            await _vaccineRepository.AddAsync(vaccine);
            await _vaccineRepository.SaveChangesAsync();

            vaccineDto.VaccineID = vaccine.VaccineID;
            return CreatedAtAction(nameof(GetVaccineById), new { id = vaccine.VaccineID }, vaccineDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVaccine(int id, [FromBody] VaccineDto vaccineDto)
        {
            if (id != vaccineDto.VaccineID) return BadRequest();

            var vaccine = await _vaccineRepository.GetByIdAsync(id);
            if (vaccine == null) return NotFound();

            vaccine.VaccineName = vaccineDto.VaccineName;
            vaccine.PeriodDays = vaccineDto.PeriodDays;

            _vaccineRepository.Update(vaccine);
            await _vaccineRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccine(int id)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(id);
            if (vaccine == null) return NotFound();

            _vaccineRepository.Remove(vaccine);
            await _vaccineRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
