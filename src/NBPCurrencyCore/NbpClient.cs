using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NBPCurrencyCore.Models;
using Newtonsoft.Json;

namespace NBPCurrencyCore
{
    public class NbpClient
    {
        public int GetSeriesOfExchangeRatesTopCount { get; } = 255;
        public int MaximumNumberOfDaysForDownloadingData { get; } = 93;
        public DateTime CurrencyExchangeRatesCollectionStartDate { get; } = new DateTime(2002, 1, 2);
        public TimeSpan TableCUpdateTime { get; } = new TimeSpan(8, 15, 0); // table C is updated every Polish business day at 8.15 AM
        public TimeZoneInfo ApiTimeZone { get; } = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
        private readonly string ApiUrl = "https://api.nbp.pl/api/";
        private readonly HttpClient httpClient;

        public NbpClient()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiUrl)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ExchangeRatesTable> GetCurrentTable()
        {
            HttpResponseMessage response = await httpClient.GetAsync("exchangerates/tables/c/");
            ExchangeRatesTable currentTable = null;

            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();

                currentTable = JsonConvert.DeserializeObject<List<ExchangeRatesTable>>(body)[0];
            }

            return currentTable;
        }

        public async Task<CurrencyInfo> GetSeriesOfLatestExchangeRates(string currencyCode,
                                                                        int topCount = 1)
        {
            if (topCount < 1 || topCount > GetSeriesOfExchangeRatesTopCount)
            {
                throw new ArgumentException(
                    $"Wrong number of exchange rates to download. Number should be between 1 and {GetSeriesOfExchangeRatesTopCount}");
            }

            HttpResponseMessage response = await httpClient.GetAsync($"exchangerates/rates/c/{currencyCode}/last/{topCount}/");

            CurrencyInfo currencyData = null;

            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();

                ApiCurrency apiData = JsonConvert.DeserializeObject<ApiCurrency>(body);
                CurrencyProcessor currencyProcessor = new CurrencyProcessor(apiData);

                currencyData = new CurrencyInfo(currencyProcessor);
            }

            return currencyData;
        }

        public async Task<CurrencyInfo> GetSeriesOfExchangeRatesFromGivenPeriod(string currencyCode,
                                                                                DateTime startDate,
                                                                                DateTime endDate)
        {
            if (startDate > endDate) // swap dates if start date is greater than end date
            {
                DateTime temp = startDate;
                startDate = endDate;
                endDate = temp;
            }

            if (startDate < CurrencyExchangeRatesCollectionStartDate)
            {
                throw new ArgumentException(
                    $"Start date cannot be earlier than {CurrencyExchangeRatesCollectionStartDate: d}");
            }

            if (endDate > DateTime.Now)
            {
                throw new ArgumentException($"End date cannot be later than current date");
            }

            TimeSpan givenPeriod = endDate - startDate;

            if (givenPeriod.Days > MaximumNumberOfDaysForDownloadingData)
            {
                throw new ArgumentException($"Given period cannot exceed {MaximumNumberOfDaysForDownloadingData} days");
            }

            HttpResponseMessage response = await httpClient.GetAsync(
                                        $"exchangerates/rates/c/{currencyCode}/{startDate: yyyy-MM-dd}/{endDate: yyyy-MM-dd}/");

            CurrencyInfo currencyData = null;

            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();

                ApiCurrency apiData = JsonConvert.DeserializeObject<ApiCurrency>(body);
                CurrencyProcessor currencyProcessor = new CurrencyProcessor(apiData);

                currencyData = new CurrencyInfo(currencyProcessor);
            }

            return currencyData;
        }
    }
}