using SEBTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEBTask.Services.Contracts
{
    public interface IExchangeRateService
    {
        Task<ExchangeRates> GetExchangeRatesByDate(DateTime date);
    }
}
