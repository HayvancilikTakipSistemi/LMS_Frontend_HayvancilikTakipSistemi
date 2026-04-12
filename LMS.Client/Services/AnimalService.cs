using LMS.Shared.DTOs.AI;
using LMS.Shared.Entities;
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

        public async Task<List<Hayvan>?> GetAnimalsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Hayvan>>("api/hayvanlar");
        }

        public async Task<Hayvan?> GetAnimalByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Hayvan>($"api/hayvanlar/{id}");
        }

        public async Task<Hayvan?> AddAnimalAsync(Hayvan animal)
        {
            var response = await _httpClient.PostAsJsonAsync("api/hayvanlar", animal);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Hayvan>();
            }
            return null;
        }

        public async Task<bool> DeleteAnimalAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/hayvanlar/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<AIAnalysisResultDto?> AnalyzeAnimalAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AIAnalysisResultDto>($"api/hayvanlar/{id}/analyze");
        }
    }
}
