using System;
using System.Net.Http;
using System.Threading.Tasks;
using NbrbAPI.Models;


namespace FinancialAssistant
{
    class RatesApi : IApi<Rate[]>
    {
        public async Task<Rate[]> Load()
        {
            string url = @"https://www.nbrb.by/api/exrates/rates?periodicity=0";
            ApiHelper.InitializeClient();

            using (HttpResponseMessage response =await ApiHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    var rates = await response.Content.ReadAsAsync<Rate[]>();
                    Logger.Log.Info("Rates were loaded via API");
                    return rates;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<Currency[]> LoadCurrenciesInfo()
        {
            string url = @"https://www.nbrb.by/api/exrates/currencies";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var currencies = await response.Content.ReadAsAsync<Currency[]>();
                    Logger.Log.Info("Currencies info were loaded via API");
                    return currencies;
                }
                else throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
