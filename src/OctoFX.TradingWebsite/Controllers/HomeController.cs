using Microsoft.AspNet.Mvc;
using OctoFX.Core.Model;
using System.Collections.Generic;
using OctoFX.Core.Repository;
using System.Linq;
using Microsoft.Framework.Logging;
using Newtonsoft.Json;

namespace OctoFX.TradingWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExchangeRateRepository exchangeRateRepository;
        private readonly ILogger logger;

        public HomeController(IExchangeRateRepository exchangeRateRepository, ILoggerFactory logger)
        {
            this.exchangeRateRepository = exchangeRateRepository;
            this.logger = logger.CreateLogger("HomeController");
        }

        public ActionResult Index()
        {
            var rates = exchangeRateRepository
                .GetAll()
                .ToList();
            logger.LogInformation($"Found {JsonConvert.SerializeObject(rates)}");
            
            return View(rates);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}