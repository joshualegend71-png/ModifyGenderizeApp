using ModifyGenderizeApp.Domain;
using ModifyGenderizeApp.Services.Interface;

namespace ModifyGenderizeApp.Services.Implementation
{
    public class ClassifyService : IClassifyService
    {
        private readonly IGenderizeService _genderizeService;
        public ClassifyService(IGenderizeService genderizeService)
        {
            _genderizeService = genderizeService;
        }

        public async Task<(bool IsError, string? ErrorMessage, ClassifyResultDto? Data)> ClassifyAsync(string name)
        {
            var result = await _genderizeService.GetGenderAsync(name);

            if (result == null || result.Gender == null || result.Count == 0)
            {
                return (true, "No prediction available for the provided name", null);
            }

            var isConfident = result.Probability >= 0.7 && result.Count >= 100;

            var data = new ClassifyResultDto
            {
                Name = name,
                Gender = result.Gender,
                Probability = result.Probability,
                SampleSize = result.Count,
                IsConfident = isConfident,
                ProcessedAt = DateTime.UtcNow.ToString("o")
            };

            return (false, null, data);
        }

    }
}
