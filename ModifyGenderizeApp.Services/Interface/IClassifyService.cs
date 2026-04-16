using ModifyGenderizeApp.Domain;

namespace ModifyGenderizeApp.Services.Interface
{
    public interface IClassifyService
    {
        Task<(bool IsError, string? ErrorMessage, ClassifyResultDto? Data)> ClassifyAsync(string name);
    }
}
