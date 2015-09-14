using Autofac;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using OctoFX.Core;
using OctoFX.Core.Repository;

namespace OctoFX.RateService
{
    public class Program
    {
        public Program(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile("config.Development.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        
        public IConfigurationRoot Configuration {get;set;}

        public void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(Configuration).As<IConfigurationRoot>().SingleInstance();
            builder.RegisterType<MarketExchangeRateProvider>().As<IMarketExchangeRateProvider>();
            builder.RegisterModule<PersistenceModule>();
            builder.Register(c => new RatesService(c.Resolve<IExchangeRateRepository>(), c.Resolve<IMarketExchangeRateProvider>())).AsSelf();

            var container = builder.Build();
            
            var ratesService = container.Resolve<RatesService>();
            ratesService.Start();
            
            while(true)
            {
                
            }
            
            ratesService.Stop();
        }
        
        
    }
}