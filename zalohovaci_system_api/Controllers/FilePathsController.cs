using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("{id}")]
        public FilePath GetById(int id)
        {
            FilePath? data = context.FilePaths.Where(x => x.id == id).FirstOrDefault();
            if (data == null)
            {
                return new FilePath();
            }
            return data;
        }

        [HttpGet("combo/{id}")]
        public IEnumerable<FilePath>? GetByComboId(int id, int? type)
        {
            PathToJobCombo[] combos = context.PathToJobs.Where(x => x.jobId == id).ToArray();

            if (type.HasValue)
            {
                combos = combos.Where(x => x.pathType == type).ToArray();
            }

            List<FilePath> data = new();

            foreach (PathToJobCombo combo in combos)
            {
                FilePath? path = context.FilePaths.Where(x => x.id == combo.pathId).FirstOrDefault();
                if (path != null)
                {
                    data.Add(path);
                }
            }

            if (data == null)
            {
                return null;
            }
            return data;
        }
    }
}
