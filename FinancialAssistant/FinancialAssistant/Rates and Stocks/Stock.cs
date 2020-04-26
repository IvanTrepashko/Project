using Newtonsoft.Json;
using System;

namespace FinancialAssistant
{
    public partial class Stock
    {
        [JsonProperty(PropertyName = "Global Quote")]
        public StockInformation StockInfo { get; set; }

        public void ShowStockInfo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{StockInfo.Symbol}\n");
            Console.ResetColor();
            Console.Write("Opened : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{StockInfo.Open}");
            Console.ResetColor();
            Console.Write("Closed : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{StockInfo.Price}");
            Console.ResetColor();
            Console.Write("Previously closed : ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(StockInfo.Previously_Close);
            Console.ResetColor();
        }
    }
}
