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
    }
}
