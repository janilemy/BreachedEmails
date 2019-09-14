using BreachedEmails.SmartCache.Interfaces;
using Orleans;

namespace BreachedEmails.SmartCache.Grains
{
    /// <summary>
    /// Orleans grain communication EmailsGrain implementation
    /// </summary>
	public class EmailsGrain : Grain, IEmailsGrain
    {
        /// <summary>
        /// Emails grain default constructor
        /// </summary>
        public EmailsGrain()
        {
            // TODO: Implement email grain constructor, add DI
        }

        // TODO: Implement email grain methods
    }
}
