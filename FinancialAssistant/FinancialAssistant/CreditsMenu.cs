using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialAssistant
{
    public static class CreditsMenu
    {
        public static void MainMenu()
        {
            CreditsRepository creditsRepository = new CreditsRepository();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Credits menu.\n\nWhat do you want to do?");
                Console.WriteLine("1. See all my credits.");
                Console.WriteLine("2. Add new credit.");
                Console.WriteLine("3. Delete credit.");
                Console.WriteLine("4. Go back.");

                int.TryParse(Console.ReadLine(), out int choice);
                switch (choice)
                {
                    case 1:
                        {
                            creditsRepository.ShowAll();
                            Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            creditsRepository.Add(Credit.Create(creditsRepository.Count+1));
                            break;
                        }
                    case 3:
                        {
                            creditsRepository.Delete();
                            break;
                        }
                    case 4:
                        {
                            creditsRepository.WriteToFile();
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
