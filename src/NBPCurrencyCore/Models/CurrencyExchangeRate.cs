using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NBPCurrencyCore.Models
{
    public class CurrencyExchangeRate
    {
        [JsonProperty("no")]
        public string TableNumber { get; set; }

        [JsonProperty("effectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("bid")]
        public decimal Bid { get; set; }

        [JsonProperty("ask")]
        public decimal Ask { get; set; }
    }
}