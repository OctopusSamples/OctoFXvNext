using System;
using System.Linq;
using System.Threading;
using OctoFX.Core.Repository;

namespace OctoFX.RateService
{
    partial class RatesService
    {

        readonly IMarketExchangeRateProvider rateProvider;
        readonly IExchangeRateRepository exchangeRateRepository;
        bool isStarted = false;

        public RatesService(IExchangeRateRepository exchangeRateRepository,IMarketExchangeRateProvider rateProvider)
        {
            this.rateProvider = rateProvider;
            this.exchangeRateRepository = exchangeRateRepository;
        }

        public void Start()
        {
            Console.WriteLine("Rate Service started...");
            isStarted = true;
            while (isStarted)
            {
                Thread.Sleep(5000);
                GenerateNewRates();
            }
        }

        public void Stop()
        {
            Console.WriteLine("Rate Service stopped...");
            isStarted = false;
        }

        void GenerateNewRates()
        {
            Console.WriteLine("Generating new rates...");
            try
            {
                var rates = exchangeRateRepository.GetAll()
                    .ToList();
                
                foreach(var rate in rates)
                {
                    rate.Rate = rateProvider.GetCurrentRate(rate.SellBuyCurrencyPair);
                    Console.WriteLine($"Rate for {rate.SellBuyCurrencyPair}: {rate.Rate.ToString("n4")}");
                    exchangeRateRepository.Update(rate);
                }
                exchangeRateRepository.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}