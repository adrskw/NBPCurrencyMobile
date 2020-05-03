using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NBPCurrencyCore.Models
{
    public class Currency
    {
        [JsonProperty("table")]
        public string TableType { get; set; }

        [JsonProperty("currency")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("rates")]
        public List<CurrencyExchangeRate> ExchangeRates { get; set; }
    }
}