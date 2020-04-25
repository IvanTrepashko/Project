using System;
using System.Threading.Tasks;


namespace FinancialAssistant
{
    class Program
    {
        public static async Task Main()
        {
            RatesRepository rates = new RatesRepository();
            await rates.CreateRepository();
            while (true)
            {
                DisplayMainMenu();
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        {
                            await RatesAndStocksMenu.MainMenuAsync(rates);
                            break;
                        }
                    default:
                        return;
                }
            }
        }

        public static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("{0,-40} {1,40}", RatesRepository.Usd, DateTime.Now);
            Console.WriteLine(RatesRepository.Eur);
            Console.WriteLine("\n\n");
            Console.WriteLine("Welcome to Financial Assistance.\nWhat do you want to do?\n");
            Console.WriteLine("1. More about currency rates and stocks");
        }
    }
}
