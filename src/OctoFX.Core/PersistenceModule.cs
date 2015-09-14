using Autofac;
using OctoFX.Core.Model;
using OctoFX.Core.Repository;

namespace OctoFX.Core
{
    public class PersistenceModule : Module
    {
        
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var context = new OctoFXContext();
            context.SeedDatabase();
            
            builder.RegisterInstance(context);
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BeneficiaryAccountRepository>().As<IBeneficiaryAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DealsRepository>().As<IDealsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExchangeRateRepository>().As<IExchangeRateRepository>().InstancePerLifetimeScope();
            builder.RegisterType<QuoteRepository>().As<IQuoteRepository>().InstancePerLifetimeScope();
        }
    }
}