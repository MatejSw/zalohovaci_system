namespace zalohovaci_system_api.Models
{
    public class ClientComplete
    {
        public int id { get; set; }
        public string pcname { get; set; }
        public string ip { get; set; }
        public List<int> jobs { get; set; }
    }
}
