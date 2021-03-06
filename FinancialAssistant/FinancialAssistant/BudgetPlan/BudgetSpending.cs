﻿using System;

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

            Logger.Log.Info("Budget spending construstor was called");

            Category = (SpendingCategory)category;
            Console.WriteLine($"Please, enter planned amount of money for {Category}.");
            while (!double.TryParse(Console.ReadLine(), out planned) || planned < 0)
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }
            PlannedAmount = planned;
            SpentAmount = 0;
        }

        public BudgetSpending(int category, double planned, double spent)
        {
            Logger.Log.Info("Budget spendings constructor was called");

            Category = (SpendingCategory)category;
            PlannedAmount = planned;
            SpentAmount = spent;
        }

        public override string ToString()
        {
            string str;
            str = $"{(int)Category};{PlannedAmount};{SpentAmount}";
            return str;
        }
    }
}
