using System.Net.Http.Json;
using LMS.Shared;

namespace LMS_Frontend_HayvancilikTakipSistemi.Services {
    public class YemService : IYemService {
        private readonly HttpClient _http;
        public YemService(HttpClient http) => _http = http;

        public async Task<string> YemVerAsync(YemKayitDto kayit) {
            var response = await _http.PostAsJsonAsync("api/yem/yem-ver", kayit);
            if (response.IsSuccessStatusCode) return "Başarıyla kaydedildi.";
            return await response.Content.ReadAsStringAsync();
        }
    }
}