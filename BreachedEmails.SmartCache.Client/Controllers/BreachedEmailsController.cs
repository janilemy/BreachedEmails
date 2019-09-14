using BreachedEmails.SmartCache.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<ActionResult<string>> Get(string email)
        {
            MailAddress emailAddress = new MailAddress(email);

            var grain = _clusterClient.GetGrain<IEmailsGrain>(emailAddress.Host);

            if (await grain.IsBreachedEmail(email))
            {
                return email;
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(string email)
        {
            try
            {
                if(string.IsNullOrEmpty(email))
                {
                    return BadRequest("Email parameter is null or empty");
                }

                MailAddress emailAddress = new MailAddress(email);

                var grain = _clusterClient.GetGrain<IEmailsGrain>(emailAddress.Host);
                if (await grain.AddBreachedEmailAsync(email))
                {
                    return Created("AddBreachedEmail", email);
                }

                return Conflict();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch(Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // TODO: log exception
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
