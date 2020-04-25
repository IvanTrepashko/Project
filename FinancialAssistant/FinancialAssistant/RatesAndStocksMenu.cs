using System;
using System.Threading.Tasks;
namespace FinancialAssistant
{
    public static class RatesAndStocksMenu
    {
        public static async Task MainMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the rates and stocks menu.\nWhat do you want to do?");
                Console.WriteLine("1. See more about rates.\n2. See more about stocks.\n3. Currency converter\n4. Go back.");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        {
                            Console.Clear();
                            RatesApi.ShowAllRates();
                            Console.Read();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            await StocksApi.InitializeStocks();
                            StocksApi.ShowAllStocks();
                            Console.Read();
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input. Please, try again");
                            break;
                        }
                }
            }
        }
    }
}
