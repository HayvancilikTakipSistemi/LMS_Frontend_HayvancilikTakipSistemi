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
    public class VaccinationRecordController : ControllerBase
    {
        private readonly IGenericRepository<VaccinationRecord> _vaccinationRecordRepository;

        public VaccinationRecordController(IGenericRepository<VaccinationRecord> vaccinationRecordRepository)
        {
            _vaccinationRecordRepository = vaccinationRecordRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaccinationRecordDto>>> GetAllVaccinationRecords()
        {
            var records = await _vaccinationRecordRepository.GetAllAsync();
            var recordDtos = records.Select(r => new VaccinationRecordDto
            {
                VaccinationRecordID = r.VaccinationRecordID,
                AnimalID = r.AnimalID,
                VeterinarianID = r.VeterinarianID,
                VaccineID = r.VaccineID,
                ApplicationDate = r.ApplicationDate,
                NextDate = r.NextDate,
                Notes = r.Notes
            });
            return Ok(recordDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VaccinationRecordDto>> GetVaccinationRecordById(int id)
        {
            var record = await _vaccinationRecordRepository.GetByIdAsync(id);
            if (record == null) return NotFound();

            var recordDto = new VaccinationRecordDto
            {
                VaccinationRecordID = record.VaccinationRecordID,
                AnimalID = record.AnimalID,
                VeterinarianID = record.VeterinarianID,
                VaccineID = record.VaccineID,
                ApplicationDate = record.ApplicationDate,
                NextDate = record.NextDate,
                Notes = record.Notes
            };
            return Ok(recordDto);
        }

        [HttpPost]
        public async Task<ActionResult<VaccinationRecordDto>> CreateVaccinationRecord([FromBody] VaccinationRecordDto recordDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var record = new VaccinationRecord
            {
                AnimalID = recordDto.AnimalID,
                VeterinarianID = recordDto.VeterinarianID,
                VaccineID = recordDto.VaccineID,
                ApplicationDate = recordDto.ApplicationDate,
                NextDate = recordDto.NextDate,
                Notes = recordDto.Notes
            };

            await _vaccinationRecordRepository.AddAsync(record);
            await _vaccinationRecordRepository.SaveChangesAsync();

            recordDto.VaccinationRecordID = record.VaccinationRecordID;
            return CreatedAtAction(nameof(GetVaccinationRecordById), new { id = record.VaccinationRecordID }, recordDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVaccinationRecord(int id, [FromBody] VaccinationRecordDto recordDto)
        {
            if (id != recordDto.VaccinationRecordID) return BadRequest();

            var record = await _vaccinationRecordRepository.GetByIdAsync(id);
            if (record == null) return NotFound();

            record.AnimalID = recordDto.AnimalID;
            record.VeterinarianID = recordDto.VeterinarianID;
            record.VaccineID = recordDto.VaccineID;
            record.ApplicationDate = recordDto.ApplicationDate;
            record.NextDate = recordDto.NextDate;
            record.Notes = recordDto.Notes;

            _vaccinationRecordRepository.Update(record);
            await _vaccinationRecordRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccinationRecord(int id)
        {
            var record = await _vaccinationRecordRepository.GetByIdAsync(id);
            if (record == null) return NotFound();

            _vaccinationRecordRepository.Remove(record);
            await _vaccinationRecordRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
