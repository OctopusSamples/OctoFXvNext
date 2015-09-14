using OctoFX.Core.Model;

namespace OctoFX.Core.Repository
{
	public interface IQuoteRepository : IOctoFXRepository<Quote>
	{
		
	}
	
	public class QuoteRepository : OctoFXRepository<Quote>, IQuoteRepository
	{
		public QuoteRepository(OctoFXContext context)
		{
			Context = context;
		}
	}
}