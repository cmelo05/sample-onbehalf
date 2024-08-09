namespace NewRepositoryAPI.Models
{
    public class GithubActionsSettings
    {
        public string User { get; set; } = null!;
        public string Repository { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string ApplicationName { get; set; } = null!;
    }
}
