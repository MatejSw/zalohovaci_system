using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_api;

namespace zalohovaci_system.Services
{
    public class JobLogger
    {
        public static void Log(string message, string? jobId, string level)
        {
            string time = $"[{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}] ";

            Console.WriteLine(time + message);

            MyContext context = new MyContext();

            int? id = null;

            if (jobId != null)
            {
                id = context.BackupJobs.Where(x => x.jobId == jobId).FirstOrDefault().Id;
            }

            int clientId = context.ClientsToJobs.Where(x => x.jobId == id).FirstOrDefault().clientId;

            context.Logs.Add(new()
            {
                message = message,
                createdAt = DateTime.Now,
                jobId = id,
                clientId = clientId,
                level = level,
            });

            context.SaveChanges();
        }
    }
}
