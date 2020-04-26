﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Globalization;


namespace FinancialAssistant
{
    public class SpendingsRepository: IRepository<Spending>
    {
        public delegate void CreditPaymentAdded(double sum);

        public event CreditPaymentAdded PaymentAdded = CreditsRepository.UpdateRemainingAmount;

        private readonly CultureInfo _culture = CultureInfo.CreateSpecificCulture("be-BY");
        private List<Spending> _spendings = new List<Spending>();
        private double _totalSpent;

        public SpendingsRepository()
        {
            var data = File.ReadAllLines(@"spendings.csv");

            foreach (var spending in data.Skip(1))
            {
                var strg = spending.Split(';');
                double moneyAmount = double.Parse(strg[0]);
                DateTimeOffset date = DateTimeOffset.Parse(strg[2]);
                SpendingCategory category = (SpendingCategory)Enum.Parse(typeof(SpendingCategory), strg[1]);
                var diff = DateTimeOffset.UtcNow - date.UtcDateTime;
                if (diff.Days <= 30)
                {
                    _spendings.Add(new Spending(moneyAmount, date, category));
                    _totalSpent += moneyAmount;
                }
            }
        }

        public void Add(Spending spending)
        {
            if (spending.Category==SpendingCategory.CreditPayment)
            {
                PaymentAdded(spending.MoneyAmount);
            }
            _spendings.Add(spending);
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public List<Spending> GetAll()
        {
            return _spendings;
        }

        public void ShowAll()
        {
            Console.WriteLine(" ___________________________________________");
            Console.WriteLine("|   Money    |    Category    |     Date    |");
            Console.WriteLine(" ___________________________________________");
            foreach (var spending in _spendings)
            {
                Console.Write($"|");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{spending.MoneyAmount.ToString("C",_culture),12}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{spending.Category,16}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{spending.Date.DateTime.Date.ToLocalTime().ToString("yyyy-MM-dd"),13}");
                Console.ResetColor();
                Console.WriteLine("|");
            }
            Console.WriteLine(" ___________________________________________");
            Console.Write("Total spent : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{_totalSpent.ToString("C",_culture)}.");
            Console.ResetColor();
        }

        public void ShowAll(List<Spending> spendings, double total)
        {
            if (spendings.Count==0)
            {
                Console.WriteLine("Nothing to show.");
                return;
            }
            Console.WriteLine(" ___________________________________________");
            Console.WriteLine("|   Money    |    Category    |     Date    |");
            Console.WriteLine(" ___________________________________________");
            foreach (var spending in spendings)
            {
                Console.Write($"|");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{spending.MoneyAmount.ToString("C",_culture),12}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{spending.Category,16}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{spending.Date.DateTime.Date.ToLocalTime().ToString("yyyy-MM-dd"),13}");
                Console.ResetColor();
                Console.WriteLine("|");
            }
            Console.WriteLine(" ___________________________________________");
            Console.Write("Total spent : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{total.ToString("C", _culture)}.");
            Console.ResetColor();
        }

        public void ShowByCategory(SpendingCategory category)
        {
            double total = 0;
            var spendings = _spendings.Where(s => s.Category == category).ToList();

            foreach (var spending in spendings)
            {
                total += spending.MoneyAmount;
            }

            ShowAll(spendings,total);
        }

        public void ShowSorted(int sort)
        {
            var spendings = new List<Spending>();
            double total = 0 ;
            if (sort == 1)
            {
                spendings = _spendings.OrderByDescending(s => s.MoneyAmount).ToList();
            }
            else
            {
                spendings = _spendings.OrderBy(s => s.Category).ToList();
            }

            foreach (var spending in spendings)
            {
                total += spending.MoneyAmount;
            }

            ShowAll(spendings,total);
        }
        public void WriteToFile()
        {
            string[] strArr = new string[_spendings.Count+1];
            strArr[0] = "money;category;date";
            int index = 1;
            foreach (var spending in _spendings)
            {
                strArr[index] = spending.ToString();
                index++;
            }
            File.WriteAllLines(@"C:\Users\itrep\Desktop\project\project\FinancialAssistant\FinancialAssistant\spendings.csv", strArr);
        }
    }
}
