using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssistant
{
    public static class StocksApi
    {
        public static List<Stock> Stocks { get; set; }

        public static async Task<List<Stock>> LoadStocks()
        {
            string url;
            string apiKey="MXTV6NZL7W973PJ2";
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

        public static async Task InitializeStocks()
        {
            Stocks = await LoadStocks();
        }

        public static void ShowAllStocks()
        {
            foreach (var stock in Stocks)
            {
                stock.ShowStockInfo();
                Console.WriteLine();
            }
        }
    }
}
