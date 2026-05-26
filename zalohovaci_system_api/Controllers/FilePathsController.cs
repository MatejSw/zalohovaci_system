using Microsoft.AspNetCore.Mvc;
using zalohovaci_system_api.Models;
namespace zalohovaci_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilePathsController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public IEnumerable<FilePath> All()
        {
            IEnumerable<FilePath> data = context.FilePaths;

            return data;
        }
    }
}
