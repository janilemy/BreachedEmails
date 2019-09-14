using BreachedEmails.SmartCache.Grains;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;

namespace BreachedEmails.SmartCache.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register client in IoC container
            var orleansClient = CreateOrleansClient();
            services.AddSingleton(orleansClient);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        /// <summary>
        /// Helper method for Orleans client connection
        /// </summary>
        /// <returns>ICluster client</returns>
        private IClusterClient CreateOrleansClient()
        {
            var clientBuilder = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "BreachedEmailClusterId";
                    options.ServiceId = "BreachedEmailsServiceId";
                })
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(EmailsGrain).Assembly).WithReferences();
                })
                .ConfigureLogging(logging => logging.AddConsole());

            var client = clientBuilder.Build();

            // Will be called from startup class which is synchronous
            // wait until service is fully configured before begin a request
            client.Connect(async ex =>
            {
                Console.WriteLine("Retrying...");
                await Task.Delay(3000);
                return true;
            }).Wait();

            return client;
        }
    }
}
