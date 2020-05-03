using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NBPCurrencyCore.Models
{
    public class TableExchangeRate
    {
        [JsonProperty("currency")]
        public string CurrencyName { get; set; }

        [JsonProperty("code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("bid")]
        public decimal Bid { get; set; }

        [JsonProperty("ask")]
        public decimal Ask { get; set; }
    }
}