using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LMS.Shared.DTOs;

namespace LMS.Client.Services
{
    public class VaccinationRecordService : IVaccinationRecordService
    {
        private readonly HttpClient _httpClient;

        public VaccinationRecordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VaccinationRecordDto>> GetVaccinationRecordsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VaccinationRecordDto>>("api/VaccinationRecord") ?? new List<VaccinationRecordDto>();
        }

        public async Task<VaccinationRecordDto?> GetVaccinationRecordByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<VaccinationRecordDto>($"api/VaccinationRecord/{id}");
        }

        public async Task<VaccinationRecordDto?> AddVaccinationRecordAsync(VaccinationRecordDto record)
        {
            var response = await _httpClient.PostAsJsonAsync("api/VaccinationRecord", record);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VaccinationRecordDto>();
            }
            return null;
        }

        public async Task<bool> DeleteVaccinationRecordAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/VaccinationRecord/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
