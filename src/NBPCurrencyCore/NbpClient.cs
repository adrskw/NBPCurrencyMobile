using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NBPCurrencyCore.Models;

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
    }
}