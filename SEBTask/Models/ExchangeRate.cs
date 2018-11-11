using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SEBTask.Models
{
    public class ExchangeRate
    {
        [XmlElement("date")]
        private string DateString { get; set; }
        [XmlElement("currency")]
        public string Currency { get; set; }
        [XmlElement("quantity")]
        public int Quantity { get; set; }
        [XmlElement("rate")]
        public decimal Rate { get; set; }
        [XmlElement("unit")]
        public string Unit { get; set; }
        [XmlIgnore]
        public DateTime Date
        {
            get { return DateTime.Parse(DateString); }
        }
    }

    [XmlRoot("ExchangeRates")]
    public class ExchangeRates
    {
        [XmlElement("item")]
        public List<ExchangeRate> Items { get; set; }
    }
}
