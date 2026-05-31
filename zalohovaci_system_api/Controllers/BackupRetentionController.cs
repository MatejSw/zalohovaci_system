using Microsoft.AspNetCore.Mvc;
using zalohovaci_system_api.Models;
namespace zalohovaci_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupRetentionController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public IEnumerable<BackupRetention> All()
        {
            IEnumerable<BackupRetention> data = context.BackupRetention;

            return data;
        }

        [HttpGet("{id}")]
        public BackupRetention GetById(int id)
        {
            BackupRetention? data = context.BackupRetention.Where(x => x.id == id).FirstOrDefault();
            if (data == null)
            {
                return new BackupRetention();
            }
            return data;
        }
    }
}
