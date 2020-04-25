using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinancialAssistant
{
    public class StocksApi : IApi<List<Stock>>
    {
        private readonly string apiKey = "MXTV6NZL7W973PJ2";
        public async Task<List<Stock>> Load()
        {
            string url;
            string[] stockTickets = { "AAPL", "BRK.A", "INTC", "MSFT", "HOG" };
            List<Stock> stocks = new List<Stock>();
            
            ApiHelper.InitializeClient();

            foreach (var ticket in stockTickets)
            {
                url = @$"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={ticket}&apikey={apiKey}";

                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var stock = await response.Content.ReadAsAsync<Stock>();
                        stocks.Add(stock);
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            return stocks;
        }
    }
}
