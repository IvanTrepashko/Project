﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinancialAssistant
{
    class StocksRepository:IRepository<Stock>
    {
        public List<Stock> Stocks { get; set; }

        public async Task CreateRepository()
        {
            StocksApi api = new StocksApi();
            Stocks = await api.Load();
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
