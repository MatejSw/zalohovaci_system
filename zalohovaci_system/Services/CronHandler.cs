using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronos;

namespace zalohovaci_system.Services
{
    public class CronHandler
    {
        public DateTime? GetNextOccurrence(string cronExpression, DateTime from)
        {
            var cron = CronExpression.Parse(cronExpression);
            return cron.GetNextOccurrence(from);
        }
    }
}
