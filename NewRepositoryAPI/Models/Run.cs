namespace NewRepositoryAPI.Models
{
    public class Run
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public List<string> Logs { get; set; }
    }
}
