using Microsoft.AspNetCore.Mvc;

namespace BreachedEmails.SmartCache.Client.Controllers
{
    [Route("api/[controller]")]
    public class BreachedEmailsController : Controller
    {
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
