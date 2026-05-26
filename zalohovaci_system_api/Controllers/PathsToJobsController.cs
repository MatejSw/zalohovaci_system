using Microsoft.AspNetCore.Mvc;
using zalohovaci_system_api.Models;
namespace zalohovaci_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PathsToJobsController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public IEnumerable<PathToJobCombo> All()
        {
            IEnumerable<PathToJobCombo> data = context.PathToJobs;

            return data;
        }
    }
}
