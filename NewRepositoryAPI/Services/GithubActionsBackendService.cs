using Microsoft.Extensions.Options;
using NewRepositoryAPI.Models;
using System.Text.Json;

namespace NewRepositoryAPI.Services
{
    public class GithubActionsBackendService : IBackendService
    {
        private readonly ILogger<GithubActionsBackendService> _logger;
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _githubActionsUrl = "https://api.github.com/";
        private readonly string _dispatchesUrl = "repos/{0}/{1}/actions/workflows/{2}/dispatches"; // repos/{owner}/{repo}/actions/workflows/{workflow_file}/dispatches
        private readonly string _workflowRunsUrl = "repos/{0}/{1}/actions/runs"; // repos/{owner}/{repo}/actions/runs
        private IOptions<GithubActionsSettings> _settings;
        public GithubActionsBackendService(ILogger<GithubActionsBackendService> logger, IOptions<GithubActionsSettings> settings)
        {
            this._logger = logger;
            this._settings = settings;
        }

        public async Task<WorkflowRun> CreateAsync(string authHeader, string repositoryName)
        {
            var dispatchUrl = string.Format(_dispatchesUrl, _settings.Value.User, _settings.Value.Repository, _settings.Value.Action);
            var url = string.Format("{0}{1}", _githubActionsUrl, dispatchUrl);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", string.Format("Bearer {0}", authHeader));
            request.Headers.Add("Accept", "application/vnd.github+json");
            request.Headers.Add("X-GitHub-Api-Version", "2022-11-28");
            request.Headers.Add("User-Agent", _settings.Value.ApplicationName);

            var internalRunId = Guid.NewGuid().ToString();

            var workflowRequest = new WorkflowRequest
            {
                Ref = "main",
                Inputs = new WorkflowRequestInputs
                {
                    RepositoryName = repositoryName,
                    InternalRunId = internalRunId,
                }
            };

            request.Content = new StringContent(JsonSerializer.Serialize(workflowRequest));
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("GithubActionsBackendService.CreateAsync: Response: {0}", responseContent);

            int count = 0;
            WorkflowRun? workflowRun = null;

            do
            {
                workflowRun = await GetWorkflowRunAsync(authHeader, internalRunId);
            } while (workflowRun == null && count++ < 5);

            if (workflowRun == null)
            {
                throw new KeyNotFoundException("Workflow run not found");
            }

            return workflowRun;
        }

        private async Task<WorkflowRun?> GetWorkflowRunAsync(string authHeader, string internalId)
        {
            var workflowRunsUrl = string.Format(_workflowRunsUrl, _settings.Value.User, _settings.Value.Repository);
            var url = string.Format("{0}{1}", _githubActionsUrl, workflowRunsUrl);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", string.Format("Bearer {0}", authHeader));
            request.Headers.Add("Accept", "application/vnd.github+json");
            request.Headers.Add("X-GitHub-Api-Version", "2022-11-28");
            request.Headers.Add("User-Agent", _settings.Value.ApplicationName);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("GithubActionsBackendService.CreateAsync: Response: {0}", responseContent);

            var workflowRunResponse = JsonSerializer.Deserialize<WorkflowRunResponse>(responseContent);

            var ghRun = workflowRunResponse.workflow_runs.FirstOrDefault(x => x.name.EndsWith(internalId));

            if (ghRun == null)
            {
                return null;
            }

            return new WorkflowRun
            {
                Id = ghRun.id,
                Logs = new List<string>(),
                Status = ghRun.status,
            };

        }
    }
}
