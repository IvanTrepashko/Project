using NbrbAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using System.Collections;

namespace FinancialAssistant
{
    public class RatesRepository : IRepository<Rate>, IEnumerable<Rate>
    {
        private readonly int _usdId=145;
        private readonly int _eurId = 292;
        public int Count { get; private set; }
        public static List<Rate> Rates { get; set; }
        public static Rate Usd { get; private set; }
        public static Rate Eur { get; private set; }

        public Rate this  [int index]
        {
            get
            {
                return Rates[index];
            }
        }
        

        public async Task CreateRepository()
        {
            RatesApi api = new RatesApi();
            var rates = await api.Load();
            Rates = rates.ToList();

            var currencies = await api.LoadCurrenciesInfo();
            var curr = currencies.ToList();

            foreach (var rate in Rates)
            {
                var tmp = curr.Find(c => c.Cur_ID == rate.Cur_ID);
                rate.Cur_Name = tmp?.Cur_Name_Eng;
            }

            Usd = Rates.Find(x => x.Cur_ID == _usdId);
            Eur = Rates.Find(x => x.Cur_ID == _eurId);
            Count = Rates.Count;
        }

        public Rate GetById(int id)
        {
            return Rates[id - 1];
        }

        public IEnumerator<Rate> GetEnumerator()
        {
            return Rates.GetEnumerator();
        }

        public void ShowAll()
        {
            Console.WriteLine($"Currency rates on {DateTime.Now.Date.ToString("yyyy-MM-dd")}:");
            Console.WriteLine();
            for (int i = 0, j = Rates.Count / 2; i < Rates.Count / 2 && j < Rates.Count; i++, j++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Currency name : {0,-30} Currency name : {1}", Rates[i].Cur_Name, Rates[j].Cur_Name);
                Console.ForegroundColor = ConsoleColor.Cyan;
                string rate1 = Rates[i].Cur_Scale + " " + Rates[i].Cur_Abbreviation + " - " + Rates[i].Cur_OfficialRate.ToString("C", CultureInfo.CreateSpecificCulture("be-BY"));
                string rate2 = Rates[j].Cur_Scale + " " + Rates[j].Cur_Abbreviation + " - " + Rates[j].Cur_OfficialRate.ToString("C", CultureInfo.CreateSpecificCulture("be-BY"));
                Console.WriteLine("Rate : {0,-38}  Rate : {1} ", rate1, rate2);
                Console.ResetColor();
                Console.WriteLine();
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
