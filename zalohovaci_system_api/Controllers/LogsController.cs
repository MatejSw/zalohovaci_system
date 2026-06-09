using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zalohovaci_system_api.Models;

namespace zalohovaci_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public IEnumerable<Log> All()
        {
            IEnumerable<Log> data = context.Logs;

            return data;
        }

        [HttpGet("job/{id}")]
        public IEnumerable<Log> GetByJobId(int id)
        {
            IEnumerable<Log> data = context.Logs.Where(x => x.jobId == id);

            return data;
        }

        [HttpGet("client/{id}")]
        public IEnumerable<Log> GetByClientId(int id)
        {
            IEnumerable<Log> data = context.Logs.Where(x => x.clientId == id);

            return data;
        }
    }
}
