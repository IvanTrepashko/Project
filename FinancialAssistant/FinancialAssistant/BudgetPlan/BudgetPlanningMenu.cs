using System;


namespace FinancialAssistant
{
    public static class BudgetPlanningMenu
    {
        public static void MainMenu()
        {
            BudgetPlan budgetPlan = new BudgetPlan();
            while (true)
            {
                Console.Clear();
                Hints.GetRandomHint();
                Console.WriteLine("\n\n");
                Console.WriteLine("Welcome to budget planning menu.\n\nWhat do you want to do?\n");
                Console.WriteLine("1. See my current budget plan.\n2. Create new budget plan.\n3. Delete current budget plan.\n4. See some budget optimization methods.\n5. Go back.");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        {
                            budgetPlan.Show();
                            break;
                        }
                    case 2:
                        {
                            Logger.Log.Info("New budget plan was created");

                            budgetPlan = new BudgetPlan(0);
                            break;
                        }
                    case 3:
                        {
                            budgetPlan.Delete();
                            break;
                        }
                    case 4:
                        {
                            BudgetPlan.BudgetPlanningMethods();
                            break;
                        }
                    case 5:
                        {
                            budgetPlan?.Dispose();
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input. Please, try again.");
                            break;
                        }
                }
            }
        }
    }
}
