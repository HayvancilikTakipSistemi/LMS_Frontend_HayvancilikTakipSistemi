using LMS.Shared.DTOs.AI;
using LMS.Shared.DTOs;
using System.Net.Http.Json;

namespace LMS.Client.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly HttpClient _httpClient;

        public AnimalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AnimalDto>?> GetAnimalsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AnimalDto>>("api/Animal");
        }

        public async Task<AnimalDto?> GetAnimalByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AnimalDto>($"api/Animal/{id}");
        }

        public async Task<AnimalDto?> AddAnimalAsync(AnimalDto animal)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Animal", animal);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AnimalDto>();
            }
            return null;
        }

        public async Task<bool> DeleteAnimalAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Animal/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<AIAnalysisResultDto?> AnalyzeAnimalAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AIAnalysisResultDto>($"api/Animal/{id}/analyze");
        }
    }
}
