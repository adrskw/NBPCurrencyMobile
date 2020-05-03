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
        private const string ApiUrl = "https://api.nbp.pl/api/";
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
    }
}