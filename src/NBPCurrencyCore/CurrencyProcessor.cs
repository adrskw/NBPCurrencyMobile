using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using NBPCurrencyCore.Models;

namespace NBPCurrencyCore
{
    public class CurrencyProcessor
    {
        public string Code { get; }
        public string Name { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public IEnumerable<CurrencyExchangeRate> ExchangeRates { get; }

        private decimal? averageBidPrice;

        public decimal AverageBidPrice
        {
            get
            {
                if (averageBidPrice == null)
                {
                    averageBidPrice = ExchangeRates.Average(rate => rate.Bid);
                }
                return averageBidPrice.Value;
            }
        }

        private decimal? minimumBidPrice;

        public decimal MinimumBidPrice
        {
            get
            {
                if (minimumBidPrice == null)
                {
                    minimumBidPrice = ExchangeRates.Min(rate => rate.Bid);
                }
                return minimumBidPrice.Value;
            }
        }

        private decimal? maximumBidPrice;

        public decimal MaximumBidPrice
        {
            get
            {
                if (maximumBidPrice == null)
                {
                    maximumBidPrice = ExchangeRates.Max(rate => rate.Bid);
                }
                return maximumBidPrice.Value;
            }
        }

        private decimal? standardDeviationOfBidPrices;

        public decimal StandardDeviationOfBidPrices
        {
            get
            {
                if (standardDeviationOfBidPrices == null)
                {
                    standardDeviationOfBidPrices = CalculateStandardDeviation(
                                                        ExchangeRates.Select(rate => rate.Bid));
                }
                return standardDeviationOfBidPrices.Value;
            }
        }

        private IEnumerable<ExchangeRate> biggestDifferencesOfBidPrices = null;

        public IEnumerable<ExchangeRate> BiggestDifferencesOfBidPrices
        {
            get
            {
                if (biggestDifferencesOfBidPrices == null)
                {
                    biggestDifferencesOfBidPrices = CalculateBiggestDifferencesOfPrices(
                                                        ExchangeRates.Select(rate => new ExchangeRate()
                                                        {
                                                            Date = rate.EffectiveDate,
                                                            Value = rate.Bid
                                                        }));
                }
                return biggestDifferencesOfBidPrices;
            }
        }

        private decimal? averageAskPrice;

        public decimal AverageAskPrice
        {
            get
            {
                if (averageAskPrice == null)
                {
                    averageAskPrice = ExchangeRates.Average(rate => rate.Ask);
                }
                return averageAskPrice.Value;
            }
        }

        private decimal? minimumAskPrice;

        public decimal MinimumAskPrice
        {
            get
            {
                if (minimumAskPrice == null)
                {
                    minimumAskPrice = ExchangeRates.Min(rate => rate.Ask);
                }
                return minimumAskPrice.Value;
            }
        }

        private decimal? maximumAskPrice;

        public decimal MaximumAskPrice
        {
            get
            {
                if (maximumAskPrice == null)
                {
                    maximumAskPrice = ExchangeRates.Max(rate => rate.Ask);
                }
                return maximumAskPrice.Value;
            }
        }

        private decimal? standardDeviationOfAskPrices;

        public decimal StandardDeviationOfAskPrices
        {
            get
            {
                if (standardDeviationOfAskPrices == null)
                {
                    standardDeviationOfAskPrices = CalculateStandardDeviation(
                                                        ExchangeRates.Select(rate => rate.Ask));
                }
                return standardDeviationOfAskPrices.Value;
            }
        }

        private IEnumerable<ExchangeRate> biggestDifferencesOfAskPrices = null;

        public IEnumerable<ExchangeRate> BiggestDifferencesOfAskPrices
        {
            get
            {
                if (biggestDifferencesOfAskPrices == null)
                {
                    biggestDifferencesOfAskPrices = CalculateBiggestDifferencesOfPrices(
                                                        ExchangeRates.Select(rate => new ExchangeRate()
                                                        {
                                                            Date = rate.EffectiveDate,
                                                            Value = rate.Ask
                                                        }));
                }
                return biggestDifferencesOfAskPrices;
            }
        }

        public CurrencyProcessor(ApiCurrency apiCurrencyData)
        {
            Code = apiCurrencyData.Code;
            Name = apiCurrencyData.Name;
            StartDate = apiCurrencyData.ExchangeRates.First().EffectiveDate;
            EndDate = apiCurrencyData.ExchangeRates.Last().EffectiveDate;
            ExchangeRates = apiCurrencyData.ExchangeRates;
        }

        private decimal CalculateStandardDeviation(IEnumerable<decimal> exchangeRates)
        {
            decimal average = exchangeRates.Average();
            decimal sumOfSquaresOfDifferences = exchangeRates.Select(
                                                rate => (rate - average) * (rate - average)).Sum();
            double standardDeviation = Math.Sqrt((double)sumOfSquaresOfDifferences / exchangeRates.Count());
            return (decimal)standardDeviation;
        }

        private IEnumerable<ExchangeRate> CalculateBiggestDifferencesOfPrices(IEnumerable<ExchangeRate> exchangeRates)
        {
            IEnumerable<ExchangeRate> biggestDifferenceExchangeRates;

            if (exchangeRates.Count() > 1)
            {
                SortedSet<ExchangeRate> exchangeRatesDifferences = new SortedSet<ExchangeRate>(
                                exchangeRates.Zip( // calculate differences between elements
                                    exchangeRates.Skip(1), (x, y) => new ExchangeRate()
                                    {
                                        Date = y.Date,
                                        Value = y.Value - x.Value
                                    }),
                                Comparer<ExchangeRate>.Create((a, b) => a.Value.CompareTo(b.Value)));

                decimal firstRate = exchangeRatesDifferences.Min.Value;
                decimal lastRate = exchangeRatesDifferences.Max.Value;

                decimal biggestDifference = Math.Max(Math.Abs(firstRate), Math.Abs(lastRate));

                biggestDifferenceExchangeRates = exchangeRatesDifferences.Where(
                                                    rate => Math.Abs(rate.Value) == biggestDifference);
            }
            else
            {
                biggestDifferenceExchangeRates = new List<ExchangeRate>{
                    new ExchangeRate()
                    {
                        Date = exchangeRates.First().Date,
                        Value = 0
                    }
                };
            }

            return biggestDifferenceExchangeRates;
        }
    }
}