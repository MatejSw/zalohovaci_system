using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zalohovaci_system.Model
{
    public class BackupJob
    {
        public List<string> Sources { get; set; }
        public List<string> Targets { get; set; }
        public string Timing { get; set; }
        public BackupRetention Retention { get; set; }
        public BackupMethod Method { get; set; }
        public DateTime NextOccurence { get; set; } = DateTime.MinValue;
        public int Id { get; set; }
    }
}
