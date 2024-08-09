using MongoDB.Bson.Serialization.Attributes;

namespace NewRepositoryAPI.Models
{
    public class WorkflowRun
    {
        [BsonId]
        public long Id { get; set; }
        public string Status { get; set; }
        public List<string> Logs { get; set; }
    }
}
