using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace OctoFX.Core.Model
{
    public class OctoFXContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BeneficiaryAccount> BeneficiaryAccounts { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Currency>();
            modelBuilder.Ignore<CurrencyPair>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = CallContextServiceLocator.Locator.ServiceProvider
                .GetService<IConfigurationRoot>();
            if(configuration == null)
            {
                var appEnv = CallContextServiceLocator.Locator.ServiceProvider.GetService<IApplicationEnvironment>();
                configuration = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                    .AddJsonFile("config.json")
                    .AddJsonFile("config.Development.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();
            }
            optionsBuilder.UseSqlServer(configuration["Data:OctoFXDatabase:ConnectionString"]);
        }


        public void SeedDatabase()
        {
            if (!ExchangeRates.Any())
            {
                foreach (var rate in initalExchangeRates)
                {
                    Set<ExchangeRate>().Add(rate);
                }
                SaveChanges();
            }
        }
        
        static ExchangeRate[] initalExchangeRates = new[]
        {
          new ExchangeRate(CurrencyPair.Parse("GBP/AUD"), 1.7217m),
          new ExchangeRate(CurrencyPair.Parse("EUR/AUD"), 1.4464m),
          new ExchangeRate(CurrencyPair.Parse("USD/AUD"), 1.0749m),
      
          new ExchangeRate(CurrencyPair.Parse("AUD/GBP"), 0.5806m),
          new ExchangeRate(CurrencyPair.Parse("EUR/GBP"), 0.8396m),
          new ExchangeRate(CurrencyPair.Parse("USD/GBP"), 0.6246m),
      
          new ExchangeRate(CurrencyPair.Parse("AUD/EUR"), 0.6916m),
          new ExchangeRate(CurrencyPair.Parse("GBP/EUR"), 1.1912m),
          new ExchangeRate(CurrencyPair.Parse("USD/EUR"), 0.7440m),
      
          new ExchangeRate(CurrencyPair.Parse("AUD/USD"), 0.9296m),
          new ExchangeRate(CurrencyPair.Parse("GBP/USD"), 1.6011m),
          new ExchangeRate(CurrencyPair.Parse("EUR/USD"), 1.3442m)
        };
    }
}