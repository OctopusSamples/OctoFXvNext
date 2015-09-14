using OctoFX.Core.Model;

namespace OctoFX.Core.Repository
{
	public interface IAccountRepository : IOctoFXRepository<Account>
	{
		
	}
	
	public class AccountRepository : OctoFXRepository<Account>, IAccountRepository
	{
		public AccountRepository(OctoFXContext context)
		{
			Context = context;
		}
	}
}