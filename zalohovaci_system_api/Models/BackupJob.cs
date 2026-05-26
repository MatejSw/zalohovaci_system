namespace zalohovaci_system_api.Models
{
    public class BackupJob
    {
        public int Id { get; set; }
        public string jobId { get; set; }
        public int retention { get; set; }
        public int method { get; set; }
        public string timing { get; set; }
        public DateTime createdAt { get; set; }
    }
}
