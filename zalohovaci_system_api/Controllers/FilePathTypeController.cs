using Microsoft.AspNetCore.Mvc;
using zalohovaci_system_api.Models;
namespace zalohovaci_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilePathTypeController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public IEnumerable<FilePathType> All()
        {
            IEnumerable<FilePathType> data = context.FilePathType;

            return data;
        }
    }
}
