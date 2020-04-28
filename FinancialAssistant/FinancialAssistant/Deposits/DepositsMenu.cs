using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace FinancialAssistant
{
    public static class DepositsMenu
    {
        public static void MainMenu()
        {
            DepositsRepository depositsRepository = new DepositsRepository();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Deposits menu.\n\nWhat do you want to do?");
                Console.WriteLine("1. See all my deposits.");
                Console.WriteLine("2. Add new deposit.");
                Console.WriteLine("3. Delete deposit.");
                Console.WriteLine("4. Go back.");
                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        {
                            ShowDeposits(depositsRepository);
                            break;
                        }
                    case 2:
                        {
                            depositsRepository.Add(Deposit.Create(depositsRepository.Count+1));
                            break;
                        }
                    case 3:
                        {
                            depositsRepository.Delete();
                            break;
                        }
                    case 4:
                        {
                            depositsRepository.WriteToFile();
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

        private static void ShowDeposits(DepositsRepository depositsRepository)
        {
            Console.Clear();
            int choice;
            depositsRepository.ShowAll();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. Sort by current money amount.\n2. Sort by interest rate.\n3. Sort by capitalization category.\n4. Sort by initial date.\n5. Sort by expiration date.\n6. Exit.\n");
            while (!int.TryParse(Console.ReadLine(), out choice) || !choice.IsPositive() || choice > 6)
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }
            var deposits = depositsRepository.GetAll();

            switch (choice)
            {
                case 1:
                    {
                        Console.Clear();
                        var sorted = deposits.OrderByDescending(x => x.CurrentMoney).ToList();
                        DepositsRepository.ShowAll(sorted);
                        Console.ReadLine();
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        var sorted = deposits.OrderByDescending(x => x.InterestRate).ToList();
                        DepositsRepository.ShowAll(sorted);
                        Console.ReadLine();
                        break;
                    }
                case 3:
                    {
                        Console.Clear();
                        var sorted = deposits.OrderBy(x => x.Capitalization).ToList();
                        DepositsRepository.ShowAll(sorted);
                        Console.ReadLine();
                        break;
                    }
                case 4:
                    {
                        Console.Clear();
                        var sorted = deposits.OrderBy(x => x.InitialDate).ToList();
                        DepositsRepository.ShowAll(sorted);
                        Console.ReadLine();
                        break;
                    }
                case 5:
                    {
                        Console.Clear();
                        var sorted = deposits.OrderBy(x => x.ExpirationDate).ToList();
                        DepositsRepository.ShowAll(sorted);
                        Console.ReadLine();
                        break;
                    }
                case 6:
                    {
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
