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
    public class FarmerController : ControllerBase
    {
        private readonly IGenericRepository<Farmer> _farmerRepository;
        private readonly IGenericRepository<Animal> _animalRepository;
        private readonly IGenericRepository<Barn> _barnRepository;

        public FarmerController(
            IGenericRepository<Farmer> farmerRepository,
            IGenericRepository<Animal> animalRepository,
            IGenericRepository<Barn> barnRepository)
        {
            _farmerRepository = farmerRepository;
            _animalRepository = animalRepository;
            _barnRepository = barnRepository;
        }

        // GET: api/farmer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FarmerDto>>> GetAllFarmers()
        {
            var farmers = await _farmerRepository.GetAllAsync();
            var animals = await _animalRepository.GetAllAsync();
            var barns = await _barnRepository.GetAllAsync();

            var farmerDtos = farmers.Select(f => new FarmerDto
            {
                FarmerID = f.FarmerID,
                FirstName = f.FirstName,
                LastName = f.LastName,
                Phone = f.PhoneNumber,
                Address = f.Address,
                Email = f.Email,
                RegistrationDate = f.CreatedDate,
                AnimalCount = animals.Count(a => a.FarmerID == f.FarmerID),
                BarnCount = barns.Count(b => b.FarmerID == f.FarmerID)
            });

            return Ok(farmerDtos);
        }

        // GET: api/farmer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FarmerDto>> GetFarmerById(int id)
        {
            var farmer = await _farmerRepository.GetByIdAsync(id);
            if (farmer == null)
                return NotFound();

            var animals = await _animalRepository.GetAllAsync();
            var barns = await _barnRepository.GetAllAsync();

            var farmerDto = new FarmerDto
            {
                FarmerID = farmer.FarmerID,
                FirstName = farmer.FirstName,
                LastName = farmer.LastName,
                Phone = farmer.PhoneNumber,
                Address = farmer.Address,
                Email = farmer.Email,
                RegistrationDate = farmer.CreatedDate,
                AnimalCount = animals.Count(a => a.FarmerID == farmer.FarmerID),
                BarnCount = barns.Count(b => b.FarmerID == farmer.FarmerID)
            };

            return Ok(farmerDto);
        }

        // POST: api/farmer
        [HttpPost]
        public async Task<ActionResult<FarmerDto>> CreateFarmer([FromBody] FarmerDto farmerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var farmer = new Farmer
            {
                FirstName = farmerDto.FirstName,
                LastName = farmerDto.LastName,
                PhoneNumber = farmerDto.Phone,
                Address = farmerDto.Address,
                Email = farmerDto.Email,
                CreatedDate = DateTime.Now
            };

            await _farmerRepository.AddAsync(farmer);
            await _farmerRepository.SaveChangesAsync();

            farmerDto.FarmerID = farmer.FarmerID;
            farmerDto.RegistrationDate = farmer.CreatedDate;
            farmerDto.AnimalCount = 0;
            farmerDto.BarnCount = 0;

            return CreatedAtAction(nameof(GetFarmerById), new { id = farmer.FarmerID }, farmerDto);
        }

        // PUT: api/farmer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFarmer(int id, [FromBody] FarmerDto farmerDto)
        {
            if (id != farmerDto.FarmerID)
                return BadRequest();

            var farmer = await _farmerRepository.GetByIdAsync(id);
            if (farmer == null)
                return NotFound();

            farmer.FirstName = farmerDto.FirstName;
            farmer.LastName = farmerDto.LastName;
            farmer.PhoneNumber = farmerDto.Phone;
            farmer.Address = farmerDto.Address;
            farmer.Email = farmerDto.Email;

            _farmerRepository.Update(farmer);
            await _farmerRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/farmer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFarmer(int id)
        {
            var farmer = await _farmerRepository.GetByIdAsync(id);
            if (farmer == null)
                return NotFound();

            _farmerRepository.Remove(farmer);
            await _farmerRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
