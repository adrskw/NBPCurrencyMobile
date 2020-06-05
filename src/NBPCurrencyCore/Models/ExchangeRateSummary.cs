using System;
using System.Collections.Generic;
using System.Text;

namespace NBPCurrencyCore.Models
{
    public class ExchangeRateSummary
    {
        public decimal Average { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public decimal StandardDeviation { get; set; }
        public IEnumerable<ExchangeRate> BiggestDifference { get; set; }
    }
}