using LMS.Shared.DTOs.AI;
using LMS.Shared.Entities;

namespace LMS.Client.Services
{
    public interface IAnimalService
    {
        Task<List<Hayvan>?> GetAnimalsAsync();
        Task<Hayvan?> GetAnimalByIdAsync(int id);
        Task<Hayvan?> AddAnimalAsync(Hayvan animal);
        Task<bool> DeleteAnimalAsync(int id);
        Task<AIAnalysisResultDto?> AnalyzeAnimalAsync(int id);
    }
}
