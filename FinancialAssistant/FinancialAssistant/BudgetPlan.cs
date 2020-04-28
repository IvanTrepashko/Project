using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FinancialAssistant
{
    public class BudgetPlan : IDisposable
    {
        private bool _disposed = false;

        private StreamReader textReader;
        public List<BudgetSpending> Spendings { get; set; }
        public DateTimeOffset InitialDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public double InitialMoney { get; set; } = 0;
        public double SavedMoney { get; set; } = 0;
        public double TotatSpendings { get; set; }

        public BudgetPlan()
        {
            textReader = new StreamReader(@"C:\Users\itrep\Desktop\project\project\FinancialAssistant\FinancialAssistant\budgetplan.csv");

            if (textReader.EndOfStream == true)
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
                }

                DateTimeOffset date;
                Console.WriteLine("Please, enter expiration date for this budget plan. (yyyy-mm-dd)");
                while (!DateTimeOffset.TryParse(Console.ReadLine(), out date) || date < DateTimeOffset.Now)
                {
                    Console.WriteLine("Wrong input. Please, try again.");
                }
                ExpirationDate = date;
                InitialDate = DateTimeOffset.UtcNow;
            }
            else
            {
                textReader.ReadLine();
                while (!textReader.EndOfStream)
                {
                    string line = textReader.ReadLine();
                    var data = line.Split(';');
                    int.TryParse(data[0], out int category);
                    double.TryParse(data[1], out double planned);
                    double.TryParse(data[2], out double spent);
                    Spendings.Add(new BudgetSpending(category, planned, spent));
                }
            }
        }

        public void Dispose()
        {
            textReader.Dispose();
            _disposed = true;
        }
    }
}
