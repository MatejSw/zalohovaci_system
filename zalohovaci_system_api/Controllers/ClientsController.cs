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
        public ClientComplete GetById(int id)
        {
            ClientComplete data = new()
            {
                id = context.Client.Where(x => x.id == id).FirstOrDefault()?.id ?? 0,
                pcname = context.Client.Where(x => x.id == id).FirstOrDefault()?.pcname ?? "",
                ip = context.Client.Where(x => x.id == id).FirstOrDefault()?.ip ?? ""
            };
            data.jobs = context.ClientsToJobs.Where(x => x.clientId == data.id).Select(x => x.jobId).ToList();
            return data;
        }

        [HttpPut("{id}")]
        public ClientComplete Update(int id, ClientComplete client)
        {
            Client db = context.Client.Where(x => x.id == id).FirstOrDefault() ?? new Client();

            if (db.id == id)
            {
                for (int i = 0; i < client.jobs.Count; i++)
                {
                    if (!context.ClientsToJobs.Any(x => x.clientId == id && x.jobId == client.jobs[i]))
                    {
                        context.ClientsToJobs.Add(new ClientToJobCombo() { clientId = id, jobId = client.jobs[i] });
                    }
                }

                    foreach (ClientToJobCombo combo in context.ClientsToJobs.Where(x => x.clientId == id).ToList())
                    {
                        if (!client.jobs.Any(x => x == combo.jobId))
                        {
                            context.ClientsToJobs.Remove(combo);
                        }
                    }
            }

            context.SaveChanges();
            return client;
        }
    }
}
