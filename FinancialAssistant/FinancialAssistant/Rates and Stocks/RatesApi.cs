using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NbrbAPI.Models;
using System.Linq;
using System.Globalization;


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
                    return currencies;
                }
                else throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
