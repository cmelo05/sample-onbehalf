using NewRepositoryAPI.Models;

namespace NewRepositoryAPI.Repositories
{
    public interface IRepository
    {
        Task<WorkflowRun> GetRunAsync(int id);
        Task<IList<WorkflowRun>> GetRunsAsync();
        Task<WorkflowRun> UpdateRunAsync(WorkflowRun run);
        Task CreateRunAsync(WorkflowRun run);
        Task DeleteRunAsync(WorkflowRun run);
    }
}
