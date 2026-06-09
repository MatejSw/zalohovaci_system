using Microsoft.AspNetCore.Mvc;
using zalohovaci_system_api.Models;
namespace zalohovaci_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private MyContext context = new MyContext();

        [HttpGet]
        public IEnumerable<Client> All()
        {
            IEnumerable<Client> data = context.Client;

            return data;
        }

        [HttpGet("{id}")]
        public Client GetById(int id)
        {
            Client? data = context.Client.Where(x => x.id == id).FirstOrDefault();
            if (data == null)
            {
                return new Client();
            }
            return data;
        }
    }
}
