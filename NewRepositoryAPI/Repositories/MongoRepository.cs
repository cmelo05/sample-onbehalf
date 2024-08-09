using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NewRepositoryAPI.Models;

namespace NewRepositoryAPI.Repositories
{
    public class MongoRepository : IRepository
    {
        private readonly ILogger<MongoRepository> _logger;
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<WorkflowRun> _collection;

        public MongoRepository(ILogger<MongoRepository> logger, IOptions<WorkflowDatabaseSettings> settings)
        {
            this._logger = logger;
            this._client = new MongoClient(settings.Value.ConnectionString);
            this._database = this._client.GetDatabase(settings.Value.DatabaseName);
            this._collection = this._database.GetCollection<WorkflowRun>(settings.Value.CollectionName);
        }

        public async Task CreateRunAsync(WorkflowRun run)
        {
            await this._collection.InsertOneAsync(run);
        }

        public Task DeleteRunAsync(WorkflowRun run)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkflowRun> GetRunAsync(int id)
        {
            return await this._collection.Find(run => run.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<WorkflowRun>> GetRunsAsync()
        {
            return await this._collection.Find(_ => true).ToListAsync();
        }

        public async Task<WorkflowRun> UpdateRunAsync(WorkflowRun run)
        {
            var result =  await this._collection.ReplaceOneAsync(r => r.Id == run.Id, run);
            
            if (result.IsAcknowledged && result.ModifiedCount == 1)
            {
                return run;
            }
            else
            {
                return null;
            }
        }
    }
}
