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
    }
}
