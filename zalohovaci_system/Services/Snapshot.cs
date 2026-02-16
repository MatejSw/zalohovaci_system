using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zalohovaci_system.Services
{
    public class Snapshot
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MethodType { get; set; }
    }
}
