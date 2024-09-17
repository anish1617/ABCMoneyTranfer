using ABCMoneyTransfer.Data;
using ABCMoneyTransfer.Models;
using ABCMoneyTransfer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ABCMoneyTransfer.Controllers
{
    [Authorize]
    public class TransferController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExchangeRateService _exchangeRateService;

        public TransferController(ApplicationDbContext context, ExchangeRateService exchangeRateService)
        {
            _context = context;
            _exchangeRateService = exchangeRateService;

        }

        public IActionResult Index()
        {
            var transferViewModel = new TransferViewModel
            {
                SenderFirstName = User.FindFirst("FirstName")?.Value,
                SenderMiddleName = User.FindFirst("MiddleName")?.Value,
                SenderLastName = User.FindFirst("LastName")?.Value,
                SenderAddress = User.FindFirst("Address")?.Value,
                SenderCountry = User.FindFirst("Country")?.Value,
            };
            return View(transferViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> TransferMoney(TransferViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var exchangeRate = await _exchangeRateService.GetExchangeRateOfCountry(DateTime.Now, "MYR");
                model.ExchangeRate = exchangeRate;
                model.PayoutAmount = model.TransferAmount * exchangeRate;

                var sender = new Person
                {
                    FirstName = model.SenderFirstName,
                    MiddleName = model.SenderMiddleName,
                    LastName = model.SenderLastName,
                    Address = model.SenderAddress,
                    Country = model.SenderCountry
                };

                var receiver = new Person
                {
                    FirstName = model.ReceiverFirstName,
                    MiddleName = model.ReceiverMiddleName,
                    LastName = model.ReceiverLastName,
                    Address = model.ReceiverAddress,
                    Country = model.ReceiverCountry
                };

                var transaction = new Transaction
                {
                    Sender = sender,
                    Receiver = receiver,
                    BankName = model.BankName,
                    AccountNumber = model.AccountNumber,
                    TransferAmount = model.TransferAmount,
                    ExchangeRate = model.ExchangeRate,
                    PayoutAmount = model.PayoutAmount,
                    TransactionDate = DateTime.Now
                };

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();


                TempData["TransferDetails"] = JsonConvert.SerializeObject(new TransferSuccessViewModel
                {
                    TransferAmount = model.TransferAmount,
                    ReceiverName = model.ReceiverFirstName,
                    PayoutAmount = model.PayoutAmount
                });

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetExchangeRate()
        {
            var exchangeRate = await _exchangeRateService.GetExchangeRateOfCountry(DateTime.Now, "MYR");
            return Json(new { exchangeRate });
        }
    }
}
