using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEBTask.Models;
using SEBTask.Services;
using SEBTask.Services.Contracts;

namespace SEBTask.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ExchangeRateController : Controller
    {
        protected readonly IExchangeRateService exchangeRateService;

        public ExchangeRateController(IExchangeRateService service)
        {
            exchangeRateService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetExchangeRatesChange(DateTime selectedDate)
        {
            var currentExchangeRateList = await exchangeRateService.GetExchangeRatesByDate(selectedDate);
            var previousDayExchangeRateList = await exchangeRateService.GetExchangeRatesByDate(selectedDate.AddDays(-1));

            if (currentExchangeRateList == null || previousDayExchangeRateList == null)
                return BadRequest();

            var exchangeRateChangeList = new List<ExchangeRateChange>();

            foreach(var currentExchangeRate in currentExchangeRateList.Items)
            {
                exchangeRateChangeList.Add(new ExchangeRateChange
                {
                    Currency = currentExchangeRate.Currency,
                    CurrentExchangeValue = currentExchangeRate.Rate,
                    OldExchangeValue = previousDayExchangeRateList.Items.FirstOrDefault(x => x.Currency == currentExchangeRate.Currency).Rate
                });
            }

            return Ok(exchangeRateChangeList.OrderByDescending(x => x.ExchangeValueDifference));
        }
    }
}
