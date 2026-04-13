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
    public class DrugController : ControllerBase
    {
        private readonly IGenericRepository<Drug> _drugRepository;

        public DrugController(IGenericRepository<Drug> drugRepository)
        {
            _drugRepository = drugRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrugDto>>> GetAllDrugs()
        {
            var drugs = await _drugRepository.GetAllAsync();
            var drugDtos = drugs.Select(d => new DrugDto
            {
                DrugID = d.DrugID,
                DrugName = d.DrugName,
                Unit = d.Unit,
                Description = d.Description
            });
            return Ok(drugDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DrugDto>> GetDrugById(int id)
        {
            var drug = await _drugRepository.GetByIdAsync(id);
            if (drug == null) return NotFound();

            var drugDto = new DrugDto
            {
                DrugID = drug.DrugID,
                DrugName = drug.DrugName,
                Unit = drug.Unit,
                Description = drug.Description
            };
            return Ok(drugDto);
        }

        [HttpPost]
        public async Task<ActionResult<DrugDto>> CreateDrug([FromBody] DrugDto drugDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var drug = new Drug
            {
                DrugName = drugDto.DrugName,
                Unit = drugDto.Unit,
                Description = drugDto.Description
            };

            await _drugRepository.AddAsync(drug);
            await _drugRepository.SaveChangesAsync();

            drugDto.DrugID = drug.DrugID;
            return CreatedAtAction(nameof(GetDrugById), new { id = drug.DrugID }, drugDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDrug(int id, [FromBody] DrugDto drugDto)
        {
            if (id != drugDto.DrugID) return BadRequest();

            var drug = await _drugRepository.GetByIdAsync(id);
            if (drug == null) return NotFound();

            drug.DrugName = drugDto.DrugName;
            drug.Unit = drugDto.Unit;
            drug.Description = drugDto.Description;

            _drugRepository.Update(drug);
            await _drugRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrug(int id)
        {
            var drug = await _drugRepository.GetByIdAsync(id);
            if (drug == null) return NotFound();

            _drugRepository.Remove(drug);
            await _drugRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
