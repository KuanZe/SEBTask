using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEBTask.Models
{
    public class ExchangeRateChange
    {
        public decimal OldExchangeValue { get; set; }
        public decimal CurrentExchangeValue { get; set; }
        public string Currency { get; set; }
        public decimal ExchangeValueDifference { get
            {
                return (CurrentExchangeValue - OldExchangeValue) * 100;
            }}
    }
}
