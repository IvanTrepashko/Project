using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace FinancialAssistant
{


    public class CreditsRepository : IRepository<Credit>
    {
        private List<Credit> _credits = new List<Credit>();
        private readonly CultureInfo _culture = CultureInfo.CreateSpecificCulture("be-BY");
        private readonly string _path = @"credits.csv";

        public int Count { get; private set; }

        public CreditsRepository()
        {
            try
            {
                var data = File.ReadAllLines(_path);

                foreach (var credit in data.Skip(1))
                {
                    var strg = credit.Split(';');
                    int.TryParse(strg[0], out int id);
                    double.TryParse(strg[1], out double total);
                    double.TryParse(strg[2], out double remain);
                    double.TryParse(strg[3], out double rate);
                    DateTimeOffset.TryParse(strg[4], out DateTimeOffset loan);
                    DateTimeOffset.TryParse(strg[5], out DateTimeOffset repayment);
                    _credits.Add(new Credit(id, total, remain, rate, loan, repayment));
                    Count++;
                }
            }
            catch (FileNotFoundException)
            {
                var file = File.Create(_path);
                file.Dispose();
            }
        }

        public void Add(Credit obj)
        {
            _credits.Add(obj);
            Count++;
        }

        public List<Credit> GetAll()
        {
            return _credits;
        }

        public static void UpdateRemainingAmount(double sum)
        {
            CreditsRepository creditsRepository = new CreditsRepository();
            int index;

            creditsRepository.ShowAll();

            Console.WriteLine("Please, enter credit ID.");

            while (!int.TryParse(Console.ReadLine(), out index) || !index.IsPositive())
            {
                Console.WriteLine("Wrong input. Please try again.");
            }
            
            creditsRepository._credits[index - 1].RemainingAmount -= sum;
            if(creditsRepository._credits[index-1].RemainingAmount==0)
            {
                creditsRepository._credits.RemoveAt(index - 1);
                foreach (var credit in creditsRepository._credits.Where(x => x.Id > index - 1))
                {
                    credit.Id--;
                }
            }
            creditsRepository.WriteToFile();
        }

        public void ShowAll()
        {
            if (_credits.Count==0)
            {
                Console.Clear();
                Console.WriteLine("You don't have any credits.");
                return;
            }

            Console.Clear();
            Console.WriteLine(" ________________________________________________________________________________");
            Console.WriteLine("| ID |      Total      |    Remaining    |  Rate  |  Loan Date  | Repayment Date |");
            Console.WriteLine(" ________________________________________________________________________________");
            foreach (var credit in _credits)
            {
                Console.Write($"|");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{credit.Id,4}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{credit.TotalMoneyAmount.ToString("C", _culture),17}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{credit.RemainingAmount.ToString("C", _culture),17}");
                Console.ResetColor();
                Console.Write("|");
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{credit.InterestRate.ToString("F2"),7}%");
                Console.ResetColor();
                Console.Write("|");
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{credit.LoanDate.ToLocalTime().ToString("yyyy-MM-dd"),13}");
                Console.ResetColor();
                Console.Write("|");
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{credit.RepaymentDate.ToLocalTime().ToString("yyyy-MM-dd"),16}");
                Console.ResetColor();
                Console.WriteLine("|");
            }
            Console.WriteLine(" ________________________________________________________________________________");
        }

        public void WriteToFile()
        {
            string[] strArr = new string[_credits.Count + 1];
            strArr[0] = "totalmoney;remainingmoney;interestrate;loandate;paymentdate";
            int index = 1;
            foreach (var spending in _credits)
            {
                strArr[index] = spending.ToString();
                index++;
            }
            File.WriteAllLines(_path, strArr);
        }

        public void Delete()
        {
            int choice;
            
            while (true)
            {
                if (_credits.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("You don't have any credits.");
                    Console.ReadLine();
                    return;
                }
                Console.Clear();
                ShowAll();
                Console.WriteLine("Please, enter an ID of credit you want to delete ('0' to exit).");
                while (!int.TryParse(Console.ReadLine(), out choice) || choice > _credits.Count)
                {
                    Console.WriteLine("Wrong input. Please, try again.");
                }
                if (choice == 0)
                    return;

                _credits.RemoveAt(choice-1);
                Count--;

                foreach (var credit in _credits.Where(x => x.Id > choice - 1))
                {
                    credit.Id--;
                }
            }
        }
    }
}
