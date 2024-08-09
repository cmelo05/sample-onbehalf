using System.Text.Json.Serialization;

namespace NewRepositoryAPI.Models
{
    public class WorkflowRequest
    {
        [JsonPropertyName("ref")]
        public string Ref { get; set; }

        [JsonPropertyName("inputs")]
        public WorkflowRequestInputs Inputs { get; set; }

    }

    public class WorkflowRequestInputs
    {
        [JsonPropertyName("repository_name")]
        public string RepositoryName { get; set; }

        [JsonPropertyName("internal_run_id")]
        public string InternalRunId { get; set; }
    }
}
