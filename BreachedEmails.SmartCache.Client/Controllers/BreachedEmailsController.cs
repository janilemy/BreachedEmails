using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace BreachedEmails.SmartCache.Client.Controllers
{
    [Route("api/[controller]")]
    public class BreachedEmailsController : Controller
    {
        // Orleans client
        private IClusterClient _clusterClient;

        public BreachedEmailsController(IClusterClient clusterClient)
        {
            // Inject cluster client
            _clusterClient = clusterClient;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
