using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;


namespace FinancialAssistant
{
    public enum SpendingCategory
    {
        Food=1,
        HCS,
        PublicTransport,
        Entertainment,
        CreditPayment,
        Appliance,
        Subscription,
        Clothes,
        Other
    }

    public class Spending
    {
        public double MoneyAmount { get; set; }
        public DateTimeOffset Date { get; set; }
        public SpendingCategory Category { get; set; }

        public Spending(double money, DateTimeOffset date, SpendingCategory category)
        {
            MoneyAmount = money;
            Date = date;
            Category = category;
        }

        public Spending()
        {
        }

        public static Spending Create()
        {
            Spending spending=new Spending();
            double money;

            Console.WriteLine("Enter an amount of money");
            while(!double.TryParse(Console.ReadLine(), out money) || !money.IsPositive())
            {
                Console.WriteLine("Wrong input. Please, try again");
            }

            SpendingCategory category = (SpendingCategory)Spending.ChooseCategory();
            spending.Category = category;
            spending.MoneyAmount = money;
            spending.Date = DateTimeOffset.UtcNow;
            return spending;
        }

        public override string ToString()
        {
            string str;
            str = $"{MoneyAmount};{(int)Category};{Date}";
            return str;
        }

        public static int ChooseCategory()
        {
            int cat=0;
            Console.WriteLine("Choose the category:");
            Console.WriteLine($"1. {SpendingCategory.Food}.");
            Console.WriteLine($"2. {SpendingCategory.HCS}.");
            Console.WriteLine($"3. {SpendingCategory.PublicTransport}.");
            Console.WriteLine($"4. {SpendingCategory.Entertainment}.");
            Console.WriteLine($"5. {SpendingCategory.CreditPayment}.");
            Console.WriteLine($"6. {SpendingCategory.Appliance}.");
            Console.WriteLine($"7. {SpendingCategory.Subscription}.");
            Console.WriteLine($"8. {SpendingCategory.Clothes}.");
            Console.WriteLine($"9. {SpendingCategory.Other}.");
            while (!int.TryParse(Console.ReadLine(), out cat) || cat<0)
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }
            return cat;
        }
    }
}
