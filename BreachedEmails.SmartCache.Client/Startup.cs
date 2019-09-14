using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans;

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
            var clientBuilder = new ClientBuilder();

            // TODO: Add client builder configuration

            var client = clientBuilder.Build();

            // TODO: Add client connection

            return client;
        }
    }
}
