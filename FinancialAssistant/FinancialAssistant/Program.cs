using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NbrbAPI.Models;

namespace FinancialAssistant
{
    class Program
    {
        public static async Task Main()
        {
            ApiHelper.InitializeClient();
            CurrencyProcessor processor = new CurrencyProcessor();
            var cur = await processor.LoadCurrencies();

            foreach (var currency in cur)
            {
                Console.WriteLine(currency);
                Console.WriteLine();
            }


        }
    }
}
