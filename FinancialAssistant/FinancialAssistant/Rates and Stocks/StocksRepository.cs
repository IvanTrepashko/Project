using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialAssistant
{
    class StocksRepository:IApiRepository<Stock>
    {
        public List<Stock> Stocks { get; set; }

        public async Task CreateRepository()
        {
            StocksApi api = new StocksApi();
            Stocks = await api.Load();
            Logger.Log.Info("Stocks repository was created");
        }

        public Stock GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void ShowAll()
        {
            foreach (var stock in Stocks)
            {
                stock.ShowStockInfo();
                Console.WriteLine();
            }
        }
    }
}
