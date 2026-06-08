using Microsoft.AspNetCore.Mvc;
using zalohovaci_system_api.Models;
namespace zalohovaci_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupJobsController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public IEnumerable<BackupJob> All()
        {
            IEnumerable<BackupJob> data = context.BackupJobs;

            return data;
        }

        [HttpGet("{id}")]
        public BackupJob GetById(int id)
        {
            BackupJob? data = context.BackupJobs.Where(x => x.Id == id).FirstOrDefault();
            if (data == null)
            {
                return new BackupJob();
            }
            return data;
        }

        [HttpGet("full/{id}")]
        public BackupJobComplete CompleteJobById(int id)
        {
            BackupJob? job = context.BackupJobs.Where(x => x.Id == id).FirstOrDefault();
            BackupRetention? backupRetention = context.BackupRetention.Where(x => x.id == job.retention).FirstOrDefault();
            BackupMethod? backupMethod = context.BackupMethod.Where(x => x.id == job.method).FirstOrDefault();
            List<int> sourcesIds = context.PathToJobs.Where(x => x.jobId == id && x.pathType == 1).Select(x => x.pathId).ToList();
            List<int> targetsIds = context.PathToJobs.Where(x => x.jobId == id && x.pathType == 2).Select(x => x.pathId).ToList();
            List<string> sources = context.FilePaths.Where(x => sourcesIds.Contains(x.id)).Select(x => x.filepath).ToList();
            List<string> targets = context.FilePaths.Where(x => targetsIds.Contains(x.id)).Select(x => x.filepath).ToList();

            BackupJobComplete data = new BackupJobComplete()
            {
                id = job.Id,
                jobId = job.jobId,
                method = backupMethod.methodName,
                retentionCount = backupRetention.count,
                retentionSize = backupRetention.size,
                sources = sources,
                targets = targets,
                timing = job.timing,
                createdAt = job.createdAt,
            };

            return data;
        }

        [HttpPut("{id}")]
        public BackupJobComplete Update(int id, BackupJobComplete data)
        {
            BackupJob job = context.BackupJobs.Find(id);
            
            BackupRetention retention = new()
            {
                count = data.retentionCount,
                size = data.retentionSize
            };
            bool newRetention = true;

            foreach (BackupRetention item in context.BackupRetention)
            {
                if (data.retentionCount == item.count && data.retentionSize == item.size)
                {
                    retention = item;
                    newRetention = false;
                    break;
                }
            }

            if (newRetention)
            {
                context.BackupRetention.Add(retention);
                context.SaveChanges();
            }

            BackupMethod method = new()
            {
                methodName = data.method
            };

            foreach (BackupMethod item in context.BackupMethod)
            {
                if (item.methodName.ToLower() == method.methodName.ToLower()) method = item;
            }

            job.method = method.id;
            job.retention = retention.id;
            job.timing = data.timing;
            job.jobId = data.jobId;

            context.SaveChanges();

            return data;
        }
    }
}
