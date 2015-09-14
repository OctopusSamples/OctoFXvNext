using OctoFX.Core.Model;

namespace OctoFX.Core.Repository
{
	public interface IBeneficiaryAccountRepository : IOctoFXRepository<BeneficiaryAccount>
	{
		
	}
	
	public class BeneficiaryAccountRepository : OctoFXRepository<BeneficiaryAccount>, IBeneficiaryAccountRepository
	{
		public BeneficiaryAccountRepository(OctoFXContext context)
		{
			Context = context;
		}
	}
}
