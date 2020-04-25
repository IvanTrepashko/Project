using System;
using System.Threading.Tasks;


namespace FinancialAssistant
{
    class Program
    {
        public static async Task Main()
        {
            await RatesApi.InitializeRates();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("{0,-40} {1,40}", RatesApi.Usd, DateTime.Now);
                Console.WriteLine(RatesApi.Eur);
                Console.WriteLine("\n\n");
                Console.WriteLine("Welcome to Financial Assistance.\nWhat do you want to do?\n");
                Console.WriteLine("1. More about currency rates and stocks");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        {
                            await RatesAndStocksMenu.MainMenuAsync();
                            break;
                        }
                    default:
                        return;
                }
            }
        }
    }
}
