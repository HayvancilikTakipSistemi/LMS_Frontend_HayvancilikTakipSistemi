using LMS.Server.Repositories;
using LMS.Shared.DTOs;
using LMS.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LMS.Server.Data;
using Microsoft.EntityFrameworkCore;
namespace LMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnimalController : ControllerBase
    {
        private readonly IGenericRepository<Animal> _animalRepository;
        private readonly AppDbContext _context;

        public AnimalController(IGenericRepository<Animal> animalRepository, AppDbContext context)
        {
            _animalRepository = animalRepository;
            _context = context;
        }

        // GET: api/animal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAllAnimals()
        {
            var animals = await _animalRepository.GetAllAsync();
            var animalDtos = animals.Select(a => new AnimalDto
            {
                AnimalID = a.AnimalID,
                FarmerID = a.FarmerID,
                BarnID = a.BarnID,
                BreedID = a.BreedID,
                AnimalStatusID = a.AnimalStatusID,
                KupeNo = a.KupeNo,
                Gender = a.Gender,
                BirthDate = a.BirthDate,
                MotherAnimalID = a.MotherAnimalID,
                BreedName = a.Breed?.BreedName,
                BarnName = a.Barn?.BarnName,
                StatusName = a.AnimalStatus?.StatusName,
                FarmerName = $"{a.Farmer?.FirstName} {a.Farmer?.LastName}"
            });

            return Ok(animalDtos);
        }

        // GET: api/animal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDto>> GetAnimalById(int id)
        {
            var animal = await _animalRepository.GetByIdAsync(id);
            if (animal == null)
                return NotFound();

            var animalDto = new AnimalDto
            {
                AnimalID = animal.AnimalID,
                FarmerID = animal.FarmerID,
                BarnID = animal.BarnID,
                BreedID = animal.BreedID,
                AnimalStatusID = animal.AnimalStatusID,
                KupeNo = animal.KupeNo,
                Gender = animal.Gender,
                BirthDate = animal.BirthDate,
                MotherAnimalID = animal.MotherAnimalID,
                BreedName = animal.Breed?.BreedName,
                BarnName = animal.Barn?.BarnName,
                StatusName = animal.AnimalStatus?.StatusName,
                FarmerName = $"{animal.Farmer?.FirstName} {animal.Farmer?.LastName}"
            };

            return Ok(animalDto);
        }

        // GET: api/animal/5/healthhistory
        [HttpGet("{id}/healthhistory")]
        public async Task<IActionResult> GetAnimalHealthHistory(int id)
        {
            var results = new List<Dictionary<string, object>>();
            var connection = _context.Database.GetDbConnection();
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = connection.CreateCommand();
                // Varsayılan olarak @HayvanID parametresini gönderiyoruz. 
                // Eğer SP farklı bir parametre adı kullanıyorsa (ör. @AnimalID), hata yönetiminde değiştirmemiz gerekebilir.
                command.CommandText = "EXEC sp_HayvanSaglikGecmisi @HayvanID";

                var param = command.CreateParameter();
                param.ParameterName = "@HayvanID";
                param.Value = id;
                command.Parameters.Add(param);

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var colName = reader.GetName(i);
                        var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        row.Add(colName, value);
                    }
                    results.Add(row);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Stored Procedure çalıştırılırken bir hata oluştu: {ex.Message}");
            }
            finally
            {
                // Entity Framework bağlantı havuzunu yönetse de, açık olan bağlantıyı güvenli şekilde bırakmak iyidir.
                // EF Core kendi yönettiği bağlantıları genellikle Dispose sırasında kapatır ama manuel Open işlemlerine dikkat etmekte fayda var.
            }

            return Ok(results);
        }

        // POST: api/animal
        [HttpPost]
        public async Task<ActionResult<AnimalDto>> CreateAnimal([FromBody] AnimalDto animalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var animal = new Animal
            {
                FarmerID = animalDto.FarmerID,
                BarnID = animalDto.BarnID,
                BreedID = animalDto.BreedID,
                AnimalStatusID = animalDto.AnimalStatusID,
                KupeNo = animalDto.KupeNo,
                Gender = animalDto.Gender,
                BirthDate = animalDto.BirthDate,
                MotherAnimalID = animalDto.MotherAnimalID
            };

            await _animalRepository.AddAsync(animal);
            await _animalRepository.SaveChangesAsync();

            animalDto.AnimalID = animal.AnimalID;
            return CreatedAtAction(nameof(GetAnimalById), new { id = animal.AnimalID }, animalDto);
        }

        // PUT: api/animal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] AnimalDto animalDto)
        {
            if (id != animalDto.AnimalID)
                return BadRequest();

            var animal = await _animalRepository.GetByIdAsync(id);
            if (animal == null)
                return NotFound();

            animal.FarmerID = animalDto.FarmerID;
            animal.BarnID = animalDto.BarnID;
            animal.BreedID = animalDto.BreedID;
            animal.AnimalStatusID = animalDto.AnimalStatusID;
            animal.KupeNo = animalDto.KupeNo;
            animal.Gender = animalDto.Gender;
            animal.BirthDate = animalDto.BirthDate;
            animal.MotherAnimalID = animalDto.MotherAnimalID;

            _animalRepository.Update(animal);
            await _animalRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/animal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _animalRepository.GetByIdAsync(id);
            if (animal == null)
                return NotFound();

            _animalRepository.Remove(animal);
            await _animalRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
