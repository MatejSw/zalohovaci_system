using Microsoft.AspNetCore.Mvc;
using zalohovaci_system_api.Models;
namespace zalohovaci_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupMethodsController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public IEnumerable<BackupMethod> All()
        {
            IEnumerable<BackupMethod> data = context.BackupMethod;

            return data;
        }
    }
}
