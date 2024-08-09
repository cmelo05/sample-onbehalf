using NewRepositoryAPI.Models;

namespace NewRepositoryAPI.Services
{
    public interface IBackendService
    {
        Task<Run> CreateAsync(string authHeader);
    }
}
