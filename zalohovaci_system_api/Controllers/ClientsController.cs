using Microsoft.AspNetCore.Mvc;
using zalohovaci_system_api.Models;
namespace zalohovaci_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private MyContext context = new MyContext();

        //[HttpGet]
        //public IEnumerable<Client> All()
        //{
        //    IEnumerable<BackupMethod> data = context.BackupMethod;

        //    return data;
        //}

        [HttpGet("{id}")]
        public BackupMethod GetById(int id)
        {
            BackupMethod? data = context.BackupMethod.Where(x => x.id == id).FirstOrDefault();
            if (data == null)
            {
                return new BackupMethod();
            }
            return data;
        }
    }
}
