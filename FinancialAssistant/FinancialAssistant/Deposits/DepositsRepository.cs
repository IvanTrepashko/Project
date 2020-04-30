using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace FinancialAssistant
{
    class DepositsRepository : IRepository<Deposit>
    {
        private List<Deposit> _deposits = new List<Deposit>();
        private readonly CultureInfo _culture = CultureInfo.CreateSpecificCulture("be-BY");
        private readonly string _path = @"deposits.csv";
        
        public int Count { get; private set; }

        public DepositsRepository()
        {
            try
            {
                var data = File.ReadAllLines(_path);

                foreach (var credit in data.Skip(1))
                {
                    var strg = credit.Split(';');
                    int.TryParse(strg[0], out int id);
                    double.TryParse(strg[1], out double initial);
                    double.TryParse(strg[2], out double current);
                    double.TryParse(strg[3], out double rate);

                    CapitalizationType capitalization = (CapitalizationType)Enum.Parse(typeof(SpendingCategory), strg[4]);
                    DateTimeOffset.TryParse(strg[5], out DateTimeOffset initialdate);
                    DateTimeOffset.TryParse(strg[6], out DateTimeOffset expiration);

                    var diff = DateTimeOffset.UtcNow - initialdate;

                    if (capitalization == CapitalizationType.No)
                    {
                        current = initial + (initial * rate * diff.Days / (365 * 100));
                    }
                    else
                    if (capitalization == CapitalizationType.Monthly)
                    {
                        double percent = 1 + (rate * 30 / (100 * 365));
                        current = initial * Math.Pow(percent, diff.Days / 30);
                    }
                    else
                    {
                        double percent = 1 + (rate * 365 / (100 * 365));
                        current = initial * Math.Pow(percent, diff.Days / 365);
                    }

                    _deposits.Add(new Deposit(id, initial, current, rate, capitalization, initialdate, expiration));
                    Count++;
                }
            }
            catch (FileNotFoundException)
            {
                var file = File.Create(_path);
                file.Dispose();
            }
        }

        public void Add(Deposit obj)
        {
            _deposits.Add(obj);
            Count++;
        }

        public void Delete()
        {
            int choice;

            while (true)
            {
                Console.Clear();
                if (_deposits.Count==0)
                {
                    Console.WriteLine("You don't have any deposits.");
                    Console.ReadLine();
                    return;
                }
                ShowAll();
                Console.WriteLine("Please, enter an ID of credit you want to delete ('0' to exit).");
                while (!int.TryParse(Console.ReadLine(), out choice) || choice > _deposits.Count || choice < 0)
                {
                    Console.WriteLine("Wrong input. Please, try again.");
                }
                if (choice == 0)
                    return;

                _deposits.RemoveAt(choice - 1);
                Count--;

                foreach (var credit in _deposits.Where(x => x.Id > choice - 1))
                {
                    credit.Id--;
                }
            }
        }

        public List<Deposit> GetAll()
        {
            return _deposits;
        }

        public void ShowAll()
        {
            if (_deposits.Count == 0)
            {
                Console.WriteLine("You don't have any deposits.");
                return;
            }
            Console.WriteLine(" _____________________________________________________________________________________________________");
            Console.WriteLine("| ID |   Initial money   |  Current money  |  Rate  | Capitalization | Initial Date | Expiration Date |");
            Console.WriteLine(" _____________________________________________________________________________________________________");
            foreach (var deposit in _deposits)
            {
                Console.Write($"|");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.Id,4}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.InitialMoney.ToString("C", _culture),19}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.CurrentMoney.ToString("C", _culture),17}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.InterestRate.ToString("F2"),7}%");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.Capitalization,16}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.InitialDate.ToLocalTime().ToString("yyyy-MM-dd"),14}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.ExpirationDate.ToLocalTime().ToString("yyyy-MM-dd"),17}");
                Console.ResetColor();
                Console.WriteLine("|");
            }
            Console.WriteLine(" _____________________________________________________________________________________________________");
        }

        public static void ShowAll(List<Deposit> deposits)
        {
            if (deposits.Count == 0)
            {
                Console.WriteLine("You don't have any deposits.");
                return;
            }
            Console.WriteLine(" _____________________________________________________________________________________________________");
            Console.WriteLine("| ID |   Initial money   |  Current money  |  Rate  | Capitalization | Initial Date | Expiration Date |");
            Console.WriteLine(" _____________________________________________________________________________________________________");
            foreach (var deposit in deposits)
            {
                Console.Write($"|");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.Id,4}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.InitialMoney.ToString("C", CultureInfo.CreateSpecificCulture("be-BY")),19}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.CurrentMoney.ToString("C", CultureInfo.CreateSpecificCulture("be-BY")),17}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.InterestRate.ToString("F2"),7}%");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.Capitalization,16}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.InitialDate.ToLocalTime().ToString("yyyy-MM-dd"),14}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{deposit.ExpirationDate.ToLocalTime().ToString("yyyy-MM-dd"),17}");
                Console.ResetColor();
                Console.WriteLine("|");
            }
            Console.WriteLine(" _____________________________________________________________________________________________________");
        }

        public void WriteToFile()
        {
            string[] strArr = new string[_deposits.Count + 1];
            strArr[0] = "id;inital money;current money;interest rate; capitalization; inital date;expiration date";
            int index = 1;
            foreach (var spending in _deposits)
            {
                strArr[index] = spending.ToString();
                index++;
            }
            File.WriteAllLines(_path, strArr);
        }
    }
}
