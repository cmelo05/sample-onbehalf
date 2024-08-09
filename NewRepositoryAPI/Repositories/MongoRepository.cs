using NewRepositoryAPI.Models;

namespace NewRepositoryAPI.Repositories
{
    public class MongoRepository : IRepository
    {
        private readonly ILogger<MongoRepository> _logger;

        public MongoRepository(ILogger<MongoRepository> logger)
        {
            this._logger = logger;
        }

        public Task<Run> CreateRunAsync(Run run)
        {
            throw new NotImplementedException();
        }

        public Task<Run> DeleteRunAsync(Run run)
        {
            throw new NotImplementedException();
        }

        public Task<Run> GetRunAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Run>> GetRunsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Run> UpdateRunAsync(Run run)
        {
            throw new NotImplementedException();
        }
    }
}
