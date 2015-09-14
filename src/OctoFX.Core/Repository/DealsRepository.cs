using OctoFX.Core.Model;

namespace OctoFX.Core.Repository
{
	public interface IDealsRepository : IOctoFXRepository<Deal>
	{
		
	}
	
	public class DealsRepository : OctoFXRepository<Deal>, IDealsRepository
	{
		public DealsRepository(OctoFXContext context)
		{
			Context = context;
		}
	}
}