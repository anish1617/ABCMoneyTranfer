using System.Diagnostics;
using ABCMoneyTransfer.Models;
using ABCMoneyTransfer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCMoneyTransfer.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ExchangeRateService _exchangeRateService;

        public HomeController(ExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetExchangeRates(string fromDate, string toDate, int page = 1)
        {
            var payload = await _exchangeRateService.GetExchangeRatesAsync(fromDate, toDate, page);

            if (payload != null)
            {
                ViewBag.FromDate = fromDate;
                ViewBag.ToDate = toDate;
                ViewBag.ExchangeRates = payload.Rates;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = 1; // Since your response contains 1 page, you can extend this logic when needed
                return PartialView("_ExchangeRatesListPartial");
            }

            return PartialView("Error");
        }
    }
}
