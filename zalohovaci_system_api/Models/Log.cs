namespace zalohovaci_system_api.Models
{
    public class Log
    {
        public int id { get; set; }
        public int? jobId { get; set; }
        public int clientId { get; set; }
        public string level { get; set; }
        public string message { get; set; }
        public DateTime createdAt { get; set; }
    }
}
