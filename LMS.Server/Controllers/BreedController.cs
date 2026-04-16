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
    public class BreedController : ControllerBase
    {
        private readonly IGenericRepository<Breed> _breedRepository;

        public BreedController(IGenericRepository<Breed> breedRepository)
        {
            _breedRepository = breedRepository;
        }

        // GET: api/breed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreedDto>>> GetAllBreeds()
        {
            var breeds = await _breedRepository.GetAllAsync();
            var breedDtos = breeds.Select(b => new BreedDto
            {
                BreedID = b.BreedID,
                AnimalTypeID = b.AnimalTypeID,
                BreedName = b.BreedName,
                MilkCapacityLiters = b.MilkCapacityLiters,
                Description = b.Description,
                AnimalTypeName = b.AnimalType?.TypeName
            });

            return Ok(breedDtos);
        }

        // GET: api/breed/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BreedDto>> GetBreedById(int id)
        {
            var breed = await _breedRepository.GetByIdAsync(id);
            if (breed == null)
                return NotFound();

            var breedDto = new BreedDto
            {
                BreedID = breed.BreedID,
                AnimalTypeID = breed.AnimalTypeID,
                BreedName = breed.BreedName,
                MilkCapacityLiters = breed.MilkCapacityLiters,
                Description = breed.Description,
                AnimalTypeName = breed.AnimalType?.TypeName
            };

            return Ok(breedDto);
        }

        // POST: api/breed
        [HttpPost]
        public async Task<ActionResult<BreedDto>> CreateBreed([FromBody] BreedDto breedDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var breed = new Breed
            {
                AnimalTypeID = breedDto.AnimalTypeID,
                BreedName = breedDto.BreedName,
                MilkCapacityLiters = breedDto.MilkCapacityLiters,
                Description = breedDto.Description
            };

            await _breedRepository.AddAsync(breed);
            await _breedRepository.SaveChangesAsync();

            breedDto.BreedID = breed.BreedID;
            return CreatedAtAction(nameof(GetBreedById), new { id = breed.BreedID }, breedDto);
        }

        // PUT: api/breed/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBreed(int id, [FromBody] BreedDto breedDto)
        {
            if (id != breedDto.BreedID)
                return BadRequest();

            var breed = await _breedRepository.GetByIdAsync(id);
            if (breed == null)
                return NotFound();

            breed.AnimalTypeID = breedDto.AnimalTypeID;
            breed.BreedName = breedDto.BreedName;
            breed.MilkCapacityLiters = breedDto.MilkCapacityLiters;
            breed.Description = breedDto.Description;

            _breedRepository.Update(breed);
            await _breedRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/breed/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreed(int id)
        {
            var breed = await _breedRepository.GetByIdAsync(id);
            if (breed == null)
                return NotFound();

            _breedRepository.Remove(breed);
            await _breedRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
