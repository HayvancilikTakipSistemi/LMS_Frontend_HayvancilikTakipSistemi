using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.JSInterop;
using LMS.Shared.Models;

namespace LMS_Frontend_HayvancilikTakipSistemi.Services
{
    public class SutService : ISutService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public SutService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        private async Task TokeniCebineKoy()
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task SutKaydetAsync(SutKaydi kayit)
        {
            await TokeniCebineKoy();
            await _http.PostAsJsonAsync("api/sut", kayit);
        }

        public async Task<List<GunlukSutOzet>> GetRaporAsync(int ciftciId)
        {
            await TokeniCebineKoy();
            return await _http.GetFromJsonAsync<List<GunlukSutOzet>>($"api/sut/rapor/{ciftciId}") ?? new();
        }
    }
}