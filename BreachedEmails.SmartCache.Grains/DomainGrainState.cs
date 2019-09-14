using System.Collections.Generic;

namespace BreachedEmails.SmartCache.Grains
{
    /// <summary>
    /// Domain grain state class that have all the breached emails for domain
    /// </summary>
    public class DomainGrainState
    {
        /// <summary>
        /// Default constructor that creates empty emails list
        /// </summary>
        public DomainGrainState()
        {
            EmailAddress = new List<string>();
        }

        /// <summary>
        /// List of breached email addresses
        /// </summary>
        public List<string> EmailAddress { get; set; }
    }
}
