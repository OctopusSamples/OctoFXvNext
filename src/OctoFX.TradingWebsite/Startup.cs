using System;
using Autofac;
using Autofac.Framework.DependencyInjection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using OctoFX.Core;
using OctoFX.TradingWebsite.Models.ModelBinders;

namespace OctoFX.TradingWebsite
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add MVC services to the services container.
            services.AddMvc(options => 
            {
                options.ModelBinders.Add(new CurrencyBinder());
            });
            
            var builder = new ContainerBuilder();
            builder.RegisterInstance(Configuration).As<IConfigurationRoot>().SingleInstance();
            builder.RegisterModule<PersistenceModule>();
            builder.Populate(services);
            
            var container = builder.Build();
                
            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();
            return container.Resolve<IServiceProvider>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            // Configure the HTTP request pipeline.

            app.UseCookieAuthentication(options =>
            {
                options.AutomaticAuthentication = true;
                options.LoginPath = new PathString("/account/login");
            });
            
            // Add the following to the request pipeline only in development environment.
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
            //}
            //else
            //{
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                //app.UseExceptionHandler("/Home/Error");
            //}
            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }
    }
}
