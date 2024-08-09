using NewRepositoryAPI.Models;

namespace NewRepositoryAPI.Services
{
    public interface IBackendService
    {
        Task<WorkflowRun> CreateAsync(string authHeader, string repositoryName);
    }
}
