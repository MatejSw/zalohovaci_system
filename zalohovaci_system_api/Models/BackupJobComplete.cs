namespace zalohovaci_system_api.Models
{
    public class BackupJobComplete
    {
        public int id { get; set; }
        public string jobId { get; set; }
        public int retentionCount { get; set; }
        public int retentionSize { get; set; }
        public List<string> sources { get; set; }
        public List<string> targets { get; set; }
        public string method { get; set; }
        public string timing { get; set; }
        public DateTime createdAt { get; set; }
    }
}
