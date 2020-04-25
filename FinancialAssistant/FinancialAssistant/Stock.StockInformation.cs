using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialAssistant
{
    public partial class Stock
    {
        public class StockInformation
        {
            [JsonProperty(PropertyName = "01. symbol")]
            public string Symbol { get; set; }

            [JsonProperty(PropertyName = "02. open")]
            public double Open { get; set; }

            [JsonProperty(PropertyName = "05. price")]
            public double Price { get; set; }

            [JsonProperty(PropertyName = "08. previous close")]
            public double Previously_Close { get; set; }
        }
    }
}
