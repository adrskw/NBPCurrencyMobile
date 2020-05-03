using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NBPCurrencyCore.Models
{
    public class ExchangeRatesTable
    {
        [JsonProperty("table")]
        public string Type { get; set; }

        [JsonProperty("no")]
        public string Number { get; set; }

        [JsonProperty("tradingDate")]
        public DateTime TradingDate { get; set; }

        [JsonProperty("effectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("rates")]
        public List<TableExchangeRate> ExchangeRates { get; set; }
    }
}