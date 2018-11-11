using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEBTask.Models
{
    public class ExchangeRateServiceSettings
    {
        public string ExchangeRateServiceBaseUrl { get; set; }
        public int ExchangeRateServiceCacheTime { get; set; }
    }
}
