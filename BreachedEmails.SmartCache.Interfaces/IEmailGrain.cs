using Orleans;
using System.Threading.Tasks;

namespace BreachedEmails.SmartCache.Interfaces
{
    /// <summary>
    /// Orleans grain communication interface IEmailGrain
    /// </summary>
    public interface IEmailsGrain : IGrainWithStringKey
    {
        Task<bool> AddBreachedEmailAsync(string email);

        Task<bool> IsBreachedEmail(string email);
    }
}
