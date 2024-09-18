using ABCMoneyTransfer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ABCMoneyTransfer.Controllers
{
    public class BankController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BankController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}


