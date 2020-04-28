using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;


namespace FinancialAssistant
{
    public class BudgetPlan : IDisposable
    {
        private bool _disposed = false;
        private static readonly string _path = @"C:\Users\itrep\Desktop\project\project\FinancialAssistant\FinancialAssistant\budgetplan.csv";
        private readonly CultureInfo _culture = CultureInfo.CreateSpecificCulture("be-BY");
        private readonly StreamReader _textReader;

        public List<BudgetSpending> Spendings { get; set; }
        public DateTimeOffset InitialDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public double InitialMoney { get; set; } = 0;
        public double SavedMoney { get; set; } = 0;
        public double TotatSpendings { get; set; }

        public static void SpendingAdded(Spending spending)
        {
            var budgetPlan = new BudgetPlan();
            if (DateTimeOffset.Now > budgetPlan.ExpirationDate)
                return;
            var spend = budgetPlan.Spendings.Where(x => x.Category == spending.Category).ToList();
            spend[0].SpentAmount += spending.MoneyAmount;

            budgetPlan.Dispose();
        }

        public BudgetPlan(int x)
        {
            Spendings = new List<BudgetSpending>()
            {
                new BudgetSpending(1),
                new BudgetSpending(2),
                new BudgetSpending(3),
                new BudgetSpending(4),
                new BudgetSpending(5),
                new BudgetSpending(6),
                new BudgetSpending(7),
                new BudgetSpending(8),
                new BudgetSpending(9),
            };

            foreach (var spending in Spendings)
            {
                InitialMoney += spending.PlannedAmount;
                TotatSpendings += spending.SpentAmount;
            }

            SavedMoney = InitialMoney - TotatSpendings;

            DateTimeOffset date;
            Console.WriteLine("Please, enter expiration date for this budget plan. (yyyy-mm-dd)");
            while (!DateTimeOffset.TryParse(Console.ReadLine(), out date) || date < DateTimeOffset.Now)
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }
            ExpirationDate = date;
            InitialDate = DateTimeOffset.UtcNow;
        }

        public BudgetPlan()
        {
            _textReader= new StreamReader(_path);
            _textReader.ReadLine();

            if (!_textReader.EndOfStream)
            {
                Spendings = new List<BudgetSpending>();
                for (int i = 0; i < 9; i++)
                {
                    string line = _textReader.ReadLine();
                    var data = line.Split(';');
                    int.TryParse(data[0], out int category);
                    double.TryParse(data[1], out double planned);
                    double.TryParse(data[2], out double spent);
                    Spendings.Add(new BudgetSpending(category, planned, spent));
                }

                string init = _textReader.ReadLine();
                DateTimeOffset.TryParse(init, out DateTimeOffset initial);
                InitialDate = initial;

                string expir = _textReader.ReadLine();
                DateTimeOffset.TryParse(expir, out DateTimeOffset expiration);
                ExpirationDate = expiration;

                foreach (var spending in Spendings)
                {
                    InitialMoney += spending.PlannedAmount;
                    TotatSpendings += spending.SpentAmount;
                }
                SavedMoney = InitialMoney - TotatSpendings;
            }
            _textReader.Dispose();
        }

        public void Show()
        {
            Console.Clear();
            if (Spendings==null)
            {
                Console.WriteLine("You dont have a budget plan.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Here is your budget plan : \n");
            Console.Write("Initial amount of money : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{InitialMoney.ToString("C",_culture)}");
            Console.ResetColor();

            Console.Write("Total money spent : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{TotatSpendings.ToString("C",_culture)}");
            Console.WriteLine();
            Console.ResetColor();
            for (int i = 0; i < 9; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{ Spendings[i].Category} : ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{Spendings[i].SpentAmount.ToString("C",_culture)}");
                Console.Write(@"/");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{Spendings[i].PlannedAmount.ToString("C",_culture)}");

                double dif = Spendings[i].PlannedAmount - Spendings[i].SpentAmount;
                if (dif<0)
                {
                    Console.ResetColor();
                    Console.Write(" (");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(dif.ToString("C", _culture));
                    Console.ResetColor();
                    Console.WriteLine(" excess)");
                }
                else
                {
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.Write("Money saved : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{SavedMoney.ToString("C",_culture)}");
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Initial date : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{InitialDate.ToLocalTime().ToString("yyyy-MM-dd")}");
            Console.ResetColor();

            Console.Write("Expiration date : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{ExpirationDate.ToLocalTime().ToString("yyyy-MM-dd")}");
            Console.ResetColor();
            
            Console.ReadLine();

        }

        public void Delete()
        {
            Spendings = null;
        }

        public void Dispose()
        {
            if (!_disposed && Spendings!=null)
            {
                _textReader?.Dispose();
                
                var textWriter = new StreamWriter(_path);
                textWriter.WriteLine("category;planned;spent;");

                foreach (var spending in Spendings)
                {
                    textWriter.WriteLine(spending);
                }

                textWriter.WriteLine(InitialDate);
                textWriter.WriteLine(ExpirationDate);

                textWriter.Dispose();
                _disposed = true;
            }
            if(!_disposed && Spendings==null)
            {
                File.Delete(_path);
                File.Create(_path);
            }
        }
    }
}
