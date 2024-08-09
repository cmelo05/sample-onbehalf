using NewRepositoryAPI.Models;

namespace NewRepositoryAPI.Services
{
    public class GithubActionsBackendService : IBackendService
    {
        private readonly ILogger<GithubActionsBackendService> _logger;
        private static readonly HttpClient _httpClient = new HttpClient();
        public GithubActionsBackendService(ILogger<GithubActionsBackendService> logger)
        {
            this._logger = logger;
        }

        public Task<Run> CreateAsync(string authHeader)
        {
            throw new NotImplementedException();
        }
    }
}
