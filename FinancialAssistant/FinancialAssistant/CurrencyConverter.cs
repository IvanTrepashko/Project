using NbrbAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialAssistant
{
    public static class CurrencyConverter
    {
        public static void ConvertCurrency(RatesRepository rates)
        {
            Console.Clear();

            Rate rate1;
            Rate rate2;

            int number=rates.Count+1;
            int rateNumber1;
            int rateNumber2;
            double amount;
            
            Console.WriteLine("Please, choose 2 currencies");
            for (int i = 0, j = rates.Count / 2; i < (rates.Count / 2) && j < rates.Count; i++, j++)
            {
                string str1= $"{i + 1}. {rates[i].Cur_Name} ({rates[i].Cur_Abbreviation})";
                string str2 = $"{j+1}. {rates[j].Cur_Name} ({rates[j].Cur_Abbreviation})";
                Console.WriteLine($"{str1,-30} {str2}");
            }
            while(!int.TryParse(Console.ReadLine(),out rateNumber1) || rateNumber1>=number)
            {
                Console.WriteLine("Wrong input. Please, try again");
            }
            
            while(!int.TryParse(Console.ReadLine(),out rateNumber2) || rateNumber2>=number)
            {
                Console.WriteLine("Wrong input. Please, try again");
            }

            rate1 = rates.GetById(rateNumber1);
            rate2 = rates.GetById(rateNumber2);

            Console.WriteLine("Please, enter the amount of {0}",rate1.Cur_Name);
            
            while (!double.TryParse(Console.ReadLine(), out amount) || rateNumber1 >= number)
            {
                Console.WriteLine("Wrong input. Please, try again");
            }

            double converted = (rate1.Cur_OfficialRate / rate1.Cur_Scale) / (rate2.Cur_OfficialRate / rate2.Cur_Scale) * amount;
            Console.WriteLine($"{amount} {rate1.Cur_Abbreviation} is {converted:F2} {rate2.Cur_Abbreviation}.");
            Console.ReadLine();
        }
    }
}
