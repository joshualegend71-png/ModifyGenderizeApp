using ModifyGenderizeApp.Domain;

namespace ModifyGenderizeApp.Services.Interface
{
    public interface IGenderizeService
    {
        Task<GenderizedResponseDto> GetGenderAsync(string name);
    }
}
