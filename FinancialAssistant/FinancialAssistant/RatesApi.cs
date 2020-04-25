using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NbrbAPI.Models;
using System.Linq;

namespace FinancialAssistant
{
    class RatesApi
    {
        public static List<Rate> Rates { get; set; }
        public static Rate Usd { get; private set; }
        public static Rate Eur { get; private set; }

        private static async Task<Rate[]> LoadCurrencyRates()
        {
            string url = @"https://www.nbrb.by/api/exrates/rates?periodicity=0";
            ApiHelper.InitializeClient();

            using (HttpResponseMessage response =await ApiHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    var rates = await response.Content.ReadAsAsync<Rate[]>();
                    return rates;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private static async Task<Currency[]> LoadCurrenciesInfo()
        {
            string url = @"https://www.nbrb.by/api/exrates/currencies";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var currencies = await response.Content.ReadAsAsync<Currency[]>();
                    return currencies;
                }
                else throw new Exception(response.ReasonPhrase);
            }
        }

        public static async Task InitializeRates()
        {
            var result = await LoadCurrencyRates();
            Rates = result.ToList<Rate>();

            var currencies = await LoadCurrenciesInfo();
            var curr = currencies.ToList<Currency>();

            foreach (var rate in Rates)
            {
                var tmp = curr.Find(c => c.Cur_ID == rate.Cur_ID);
                rate.Cur_Name = tmp?.Cur_Name_Eng;
            }
            
            Usd = RatesApi.Rates.Find(x => x.Cur_ID == 145);
            Eur = RatesApi.Rates.Find(x => x.Cur_ID == 292);
        }

        public static void ShowAllRates()
        {
            Console.WriteLine($"Currency rates on {DateTime.Now.Date.ToString("yyyy-mm-dd")}:");
            foreach (var rate in Rates)
            {
                rate.ShowInformation();
                Console.WriteLine();
            }
        }
    }
}
