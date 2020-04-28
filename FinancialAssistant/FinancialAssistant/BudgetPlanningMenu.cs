using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace FinancialAssistant
{
    public static class BudgetPlanningMenu
    {
        public static void MainMenu()
        {
            while (true)
            {
                BudgetPlan budgetPlan = new BudgetPlan();
                Console.Clear();
                Console.WriteLine("Welcome to budget planning menu.\n\nWhat do you want to do?\n");
                Console.WriteLine("1. See my current budget plan.\n2. Create new budget plan.3. Delete current budget plan.");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            break;
                        }


                    default:
                        break;
                }
            }
        }
    }
}
