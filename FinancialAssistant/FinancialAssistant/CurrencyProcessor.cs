using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NbrbAPI.Models;


namespace FinancialAssistant
{
    class CurrencyProcessor
    {
        public async Task<Rate[]> LoadCurrencies()
        {
            string url = @"https://www.nbrb.by/api/exrates/rates?periodicity=0";


            using (HttpResponseMessage response =await ApiHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    var currency = await response.Content.ReadAsAsync<Rate[]>();
                    return currency;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
