using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialAssistant
{
    public partial class Stock
    {
        [JsonProperty(PropertyName = "Global Quote")]
        public StockInformation StockInfo { get; set; }

        public void ShowStockInfo()
        {
            Console.WriteLine($"{StockInfo.Symbol}\nOpened : {StockInfo.Open}.\nClosed : {StockInfo.Price}.\nPreviously closed : {StockInfo.Previously_Close}.");
        }
    }
}
