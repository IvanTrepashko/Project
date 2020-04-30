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
        private static readonly string _path = @"budgetplan.csv";
        private readonly CultureInfo _culture = CultureInfo.CreateSpecificCulture("be-BY");
        private readonly StreamReader _textReader;
        private static readonly string[] _methods = new string[3];

        public List<BudgetSpending> Spendings { get; set; }
        public DateTimeOffset InitialDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public double InitialMoney { get; set; } = 0;
        public double SavedMoney { get; set; } = 0;
        public double TotatSpendings { get; set; }

        public BudgetPlan(int x)
        {
            Logger.Log.Info("Budget plan constructer was called");

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
            SetMethods();
        }

        public BudgetPlan()
        {
            Logger.Log.Info("Budget plan constructor was called");

            try
            {
                _textReader = new StreamReader(_path);
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
                SetMethods();
            }
            catch (FileNotFoundException)
            {
                Logger.Log.Error("Budget plan file was not found");

                var file = File.Create(_path);
                file.Dispose();
            }
        }

        public static void SpendingAdded(Spending spending)
        {
            Logger.Log.Info("Budget information was updated");

            var budgetPlan = new BudgetPlan();
            if (DateTimeOffset.Now > budgetPlan.ExpirationDate)
                return;
            var spend = budgetPlan.Spendings.Where(x => x.Category == spending.Category).ToList();
            spend[0].SpentAmount += spending.MoneyAmount;

            budgetPlan.Dispose();
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
            Logger.Log.Info("Budget plan was deleted");
            Spendings = null;
        }

        public void Dispose()
        {
            Logger.Log.Info("Budget plan object was disposed");

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
                var file = File.Create(_path);
                file.Dispose();
            }
        }

        public static void BudgetPlanningMethods()
        {
            Console.Clear();

            Console.WriteLine("Method №1");
            Console.WriteLine(_methods[0]);

            Console.WriteLine();
            Console.WriteLine("Method №2");
            Console.WriteLine(_methods[1]);

            Console.WriteLine();
            Console.WriteLine("Method №3");
            Console.WriteLine(_methods[2]);

            Console.ReadLine();
        }

        private void SetMethods()
        {
            Logger.Log.Info("Budget planning methods were set");

            _methods[0] = "Step 1  - Get rid of loans and depts.\nStep 2 - Save and / or invest 20% of income (never spend this money).\nStep 3 - Live on the remaining 80% for your pleasure.";
            _methods[1] = "Spend 50% on necessary things (products, rent, transport, insurance, basic clothes, etc.)\nSpend 30% on desired things (cable TV, fashion clothes, jewelry, going to a restaurant, theater tickets, books, hobbies, etc.)\nSave remaining 20%";
            _methods[2] = "Current expenses - 60%.\nPension savings -10 %.\nLong - term purchases and payments - 10 %.\nIrregular expenses -10 %.\nEntertainment - 10 %.";
        }
    }
}
