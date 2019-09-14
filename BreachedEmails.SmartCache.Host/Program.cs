using BreachedEmails.SmartCache.Grains;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BreachedEmails.SmartCache.Host
{
    /// <summary>
    /// Silo startup program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Silo startup main async method - C# 7.1 supports async Main method
        /// </summary>
        /// <param name="args">Input arguments - null</param>
        /// <returns>0 when silo was successfully terminated or 1 when error occurs</returns>
        public static async Task<int> Main(string[] args)
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Silo has started. Press Enter to terminate...");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var builder = new SiloHostBuilder().UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "BreachedEmailClusterId";
                    options.ServiceId = "BreachedEmailsServiceId";
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .Configure<GrainCollectionOptions>(options =>
                {
                    options.CollectionAge = TimeSpan.FromMinutes(5);
                })
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(EmailsGrain).Assembly).WithReferences();
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .AddAzureBlobGrainStorage("blobStorage",
                    (options) =>
                    {
                        options.ConnectionString = AppSettings.GetSetting("AzureStorageConnectionString");
                    });

            var host = builder.Build();
            await host.StartAsync();

            return host;
        }
    }
}
