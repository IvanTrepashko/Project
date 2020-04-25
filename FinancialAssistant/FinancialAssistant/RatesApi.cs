using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NbrbAPI.Models;
using System.Linq;
using System.Globalization;


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
            Console.WriteLine();
            for (int i = 0, j = Rates.Count/2; i < Rates.Count/2 && j < Rates.Count;i++,j++)
            {
                Console.WriteLine("Currency name : {0,-30} Currency name : {1}",Rates[i].Cur_Name,Rates[j].Cur_Name);
                string rate1 = Rates[i].Cur_Scale + " " + Rates[i].Cur_Abbreviation + " - " + Rates[i].Cur_OfficialRate.ToString("C", CultureInfo.CreateSpecificCulture("be-BY"));
                string rate2 = Rates[j].Cur_Scale + " " + Rates[j].Cur_Abbreviation + " - " + Rates[j].Cur_OfficialRate.ToString("C", CultureInfo.CreateSpecificCulture("be-BY"));
                
                Console.WriteLine("Rate : {0,-38}  Rate : {1} ",rate1,rate2);
                Console.WriteLine();
            }
        }
    }
}
