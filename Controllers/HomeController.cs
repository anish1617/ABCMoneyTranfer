using System.Diagnostics;
using ABCMoneyTransfer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABCMoneyTransfer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _clientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

       public async Task<IActionResult> GetExchangeRates()
        {
            
            return View();
        }
    }
}
