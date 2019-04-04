using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBus.BusinessLogic;
using ServiceBus.Common.Receiver;
using ServiceBus.Common.Sender;
using ServiceBus.DataContract;
using ServiceBus.WebClient.HostedServices;

namespace ServiceBus.WebClient
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
            const string connectionString = "XXXX";
            const string topicName = "licencetopic";
            const string subscriptionName = "create";

            services.AddTransient<ITopicClient>(provider =>
            {
                return new TopicClient(connectionString, topicName);
            });

            services.AddTransient<ISubscriptionClient>(provider =>
            {
                return new SubscriptionClient(connectionString, topicName, subscriptionName);
            });

            services.AddTransient<ITopicSender, TopicSender>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<LicenceServices>();
            services.AddSingleton<ITopicReceiver, TopicReceiver>();
            services.AddHostedService<ServiceBusSubscriptionHostedService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
