using System;

namespace FinancialAssistant
{
    public static class SpendingManagementMenu
    {
        public static void MainMenu()
        {
            SpendingsRepository spendingsRepository = new SpendingsRepository();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Spending Management menu.\n\nWhat do You want to do?");
                Console.WriteLine("1. Add new spending.");
                Console.WriteLine("2. See all spendings in last 30 days.");
                Console.WriteLine("3. See spendings of specified category.");
                Console.WriteLine("4. Go back.");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        {
                            Spending tmp = Spending.Create();
                            spendingsRepository.Add(tmp);
                            break;
                        }
                    case 2:
                        {
                            ShowSpendings(spendingsRepository);
                            break;
                        }
                    case 3:
                        {
                            SpecifiedSpendings(spendingsRepository);
                            break;
                        }
                    case 4:
                        {
                            spendingsRepository.WriteToFile();
                            return;
                        }
                    default:
                        Console.WriteLine("Wrong input. Please, try again.");
                        break;
                }
            }
        }

        private static void ShowSpendings(SpendingsRepository spendingsRepository)
        {
            int choice;
            spendingsRepository.ShowAll();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Enter '1' to sort by money amount, '2' to sort by category, '3' to exit.");
            while (!int.TryParse(Console.ReadLine(), out choice) || !choice.IsPositive() || choice > 3)
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }
            Console.Clear();
            if (choice == 3)
                return;
            else
            {
                spendingsRepository.ShowSorted(choice);
                Console.Read();
            }
        }
        private static void SpecifiedSpendings(SpendingsRepository spendingsRepository)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("To exit enter 0.");
                var category = Spending.ChooseCategory();
                if (category == 0)
                    return;
                spendingsRepository.ShowByCategory((SpendingCategory)category);
                Console.ReadLine();
            }
        }
    }
}
