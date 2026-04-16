using ModifyGenderizeApp.Domain;
using ModifyGenderizeApp.Services.Interface;
using System.Text.Json;

namespace ModifyGenderizeApp.Services.Implementation
{
    public class GenderizeService : IGenderizeService
    {
        private readonly HttpClient _httpClient;
        public GenderizeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GenderizedResponseDto> GetGenderAsync(string name)
        {
            var response = await _httpClient.GetAsync($"https://api.genderize.io/?name={name}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("External API failure");

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GenderizedResponseDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
