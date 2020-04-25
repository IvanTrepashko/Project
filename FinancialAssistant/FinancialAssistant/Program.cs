using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NbrbAPI.Models;
using System.Linq;


namespace FinancialAssistant
{
    class Program
    {
        public static async Task Main()
        {
            await RatesApi.InitializeRates();
            Console.WriteLine(RatesApi.Usd + "                                                    " +DateTime.Now);
            Console.WriteLine(RatesApi.Eur);
            Console.WriteLine("\n\n");
            Console.WriteLine("Welcome back. What do you want to do?");
            Console.WriteLine("1. More about currency rates and stocks");
            Console.ReadLine();
            await RatesAndStocksMenu.MainMenuAsync();
        }
    }
}
