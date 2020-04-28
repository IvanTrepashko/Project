using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialAssistant
{
    public class BudgetSpending
    {
        public SpendingCategory Category { get; set; }
        public double PlannedAmount { get; set; }
        public double SpentAmount { get; set; }

        public BudgetSpending(int category)
        {
            double planned;

            Category = (SpendingCategory)category;
            Console.WriteLine($"Please, enter planned amount of money for {Category}.");
            while(!double.TryParse(Console.ReadLine(),out planned) || !planned.IsPositive())
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }
            PlannedAmount = planned;
            SpentAmount = 0;
        }

        public BudgetSpending(int category, double planned, double spent)
        {
            Category = (SpendingCategory)category;
            PlannedAmount = planned;
            SpentAmount = spent;
        }
    }
}
