using System.Collections;
using OctoFX.Core.Model;

namespace OctoFX.RateService
{
    public interface IMarketExchangeRateProvider
    {
        decimal GetCurrentRate(CurrencyPair sellBuyCurrencyPair);
    }
}