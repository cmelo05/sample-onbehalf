using NewRepositoryAPI.Models;

namespace NewRepositoryAPI.Repositories
{
    public interface IRepository
    {
        Task<Run> GetRunAsync(int id);
        Task<IList<Run>> GetRunsAsync(int id);
        Task<Run> UpdateRunAsync(Run run);
        Task<Run> CreateRunAsync(Run run);
        Task<Run> DeleteRunAsync(Run run);
    }
}
