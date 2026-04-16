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
    public class ExaminationController : ControllerBase
    {
        private readonly IGenericRepository<Examination> _examinationRepository;

        public ExaminationController(IGenericRepository<Examination> examinationRepository)
        {
            _examinationRepository = examinationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExaminationDto>>> GetAllExaminations()
        {
            var examinations = await _examinationRepository.GetAllAsync();
            var examinationDtos = examinations.Select(e => new ExaminationDto
            {
                ExaminationID = e.ExaminationID,
                AnimalID = e.AnimalID,
                VeterinarianID = e.VeterinarianID,
                ExaminationDate = e.ExaminationDate,
                Diagnosis = e.Diagnosis,
                Treatment = e.Treatment,
                Notes = e.Notes
            });
            return Ok(examinationDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExaminationDto>> GetExaminationById(int id)
        {
            var examination = await _examinationRepository.GetByIdAsync(id);
            if (examination == null) return NotFound();

            var examinationDto = new ExaminationDto
            {
                ExaminationID = examination.ExaminationID,
                AnimalID = examination.AnimalID,
                VeterinarianID = examination.VeterinarianID,
                ExaminationDate = examination.ExaminationDate,
                Diagnosis = examination.Diagnosis,
                Treatment = examination.Treatment,
                Notes = examination.Notes
            };
            return Ok(examinationDto);
        }

        [HttpPost]
        public async Task<ActionResult<ExaminationDto>> CreateExamination([FromBody] ExaminationDto examinationDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var examination = new Examination
            {
                AnimalID = examinationDto.AnimalID,
                VeterinarianID = examinationDto.VeterinarianID,
                ExaminationDate = examinationDto.ExaminationDate,
                Diagnosis = examinationDto.Diagnosis,
                Treatment = examinationDto.Treatment,
                Notes = examinationDto.Notes
            };

            if (examinationDto.PrescribedDrugs != null && examinationDto.PrescribedDrugs.Any())
            {
                foreach (var drug in examinationDto.PrescribedDrugs)
                {
                    examination.ExaminationDrugs.Add(new ExaminationDrug
                    {
                        DrugID = drug.DrugID,
                        Dosage = drug.Dosage,
                        ExpiryDate = drug.ExpiryDate
                    });
                }
            }

            await _examinationRepository.AddAsync(examination);
            await _examinationRepository.SaveChangesAsync();

            examinationDto.ExaminationID = examination.ExaminationID;
            return CreatedAtAction(nameof(GetExaminationById), new { id = examination.ExaminationID }, examinationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExamination(int id, [FromBody] ExaminationDto examinationDto)
        {
            if (id != examinationDto.ExaminationID) return BadRequest();

            var examination = await _examinationRepository.GetByIdAsync(id);
            if (examination == null) return NotFound();

            examination.AnimalID = examinationDto.AnimalID;
            examination.VeterinarianID = examinationDto.VeterinarianID;
            examination.ExaminationDate = examinationDto.ExaminationDate;
            examination.Diagnosis = examinationDto.Diagnosis;
            examination.Treatment = examinationDto.Treatment;
            examination.Notes = examinationDto.Notes;

            _examinationRepository.Update(examination);
            await _examinationRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamination(int id)
        {
            var examination = await _examinationRepository.GetByIdAsync(id);
            if (examination == null) return NotFound();

            _examinationRepository.Remove(examination);
            await _examinationRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
