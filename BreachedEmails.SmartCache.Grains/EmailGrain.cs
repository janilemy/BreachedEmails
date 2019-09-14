using BreachedEmails.SmartCache.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Providers;
using System.Threading.Tasks;

namespace BreachedEmails.SmartCache.Grains
{
    /// <summary>
    /// Orleans grain communication EmailsGrain implementation
    /// </summary>
    [StorageProvider(ProviderName = "blobStorage")]
    public class EmailsGrain : Grain<DomainGrainState>, IEmailsGrain
    {
        // Application logger
        private readonly ILogger _logger;

        /// <summary>
        /// Emails grain default constructor
        /// </summary>
        public EmailsGrain(ILogger<EmailsGrain> logger)
        {
            // DI for logger
            _logger = logger;
        }

        public async Task<bool> AddBreachedEmailAsync(string email)
        {
            _logger.LogInformation($"Set '{email}' as breached email!");

            if (await IsBreachedEmail(email))
            {
                // Breached email already exist
                return false;
            }
            else
            {
                State.EmailAddress.Add(email);
                await WriteStateAsync();

                return true;
            }
        }

        public Task<bool> IsBreachedEmail(string email)
        {
            _logger.LogInformation($"Checking if '{email}' is breached!");

            return Task.FromResult(State.EmailAddress.Contains(email));
        }
    }
}
