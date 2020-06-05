using System;
using System.Collections.Generic;
using System.Text;

namespace NBPCurrencyCore.Models
{
    public class CurrencyInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<CurrencyExchangeRate> ExchangeRates { get; set; }
        public ExchangeRateSummary BidSummary { get; set; }
        public ExchangeRateSummary AskSummary { get; set; }

        public CurrencyInfo()
        {
        }

        public CurrencyInfo(CurrencyProcessor currencyProcessor)
        {
            Code = currencyProcessor.Code;
            Name = currencyProcessor.Name;
            StartDate = currencyProcessor.StartDate;
            EndDate = currencyProcessor.EndDate;
            ExchangeRates = currencyProcessor.ExchangeRates;

            BidSummary = new ExchangeRateSummary()
            {
                Average = currencyProcessor.AverageBidPrice,
                Minimum = currencyProcessor.MinimumBidPrice,
                Maximum = currencyProcessor.MaximumBidPrice,
                StandardDeviation = currencyProcessor.StandardDeviationOfBidPrices,
                BiggestDifference = currencyProcessor.BiggestDifferencesOfBidPrices
            };

            AskSummary = new ExchangeRateSummary()
            {
                Average = currencyProcessor.AverageAskPrice,
                Minimum = currencyProcessor.MinimumAskPrice,
                Maximum = currencyProcessor.MaximumAskPrice,
                StandardDeviation = currencyProcessor.StandardDeviationOfAskPrices,
                BiggestDifference = currencyProcessor.BiggestDifferencesOfAskPrices
            };
        }
    }
}