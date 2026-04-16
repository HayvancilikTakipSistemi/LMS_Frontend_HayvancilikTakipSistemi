using LMS.Shared.DTOs.AI;
using LMS.Shared.DTOs;

namespace LMS.Client.Services
{
    public interface IAnimalService
    {
        Task<List<AnimalDto>?> GetAnimalsAsync();
        Task<AnimalDto?> GetAnimalByIdAsync(int id);
        Task<AnimalDto?> AddAnimalAsync(AnimalDto animal);
        Task<bool> DeleteAnimalAsync(int id);
        Task<AIAnalysisResultDto?> AnalyzeAnimalAsync(int id);
    }
}
