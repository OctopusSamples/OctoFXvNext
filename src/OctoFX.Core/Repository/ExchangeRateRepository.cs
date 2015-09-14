using OctoFX.Core.Model;

namespace OctoFX.Core.Repository
{
	public interface IExchangeRateRepository : IOctoFXRepository<ExchangeRate>
	{
		
	}
	
	public class ExchangeRateRepository : OctoFXRepository<ExchangeRate>, IExchangeRateRepository
	{
		public ExchangeRateRepository(OctoFXContext context)
		{
			Context = context;
		}
	}
}