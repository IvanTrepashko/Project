﻿using System;

namespace FinancialAssistant
{
    public enum CapitalizationType
    {
        No=1,
        Monthly,
        Yearly
    }

    public class Deposit
    {
        public int Id { get; set; }
        public double InitialMoney { get; set; }
        public double CurrentMoney { get; set; }
        public double InterestRate { get; set; }
        public CapitalizationType Capitalization { get; set; }
        public DateTimeOffset InitialDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }

        public Deposit(int id, double initial, double current, double rate, CapitalizationType capitalization, DateTimeOffset initialdate, DateTimeOffset expiration)
        {
            Logger.Log.Info("Deposit constructor was called");

            Id = id;
            InitialMoney = initial;
            CurrentMoney = current;
            InterestRate = rate;
            Capitalization = capitalization;
            InitialDate = initialdate;
            ExpirationDate = expiration;
        }

        public static Deposit Create(int id)
        {
            Logger.Log.Info("Deposit was created");

            double initial;
            double current;
            double rate;
            CapitalizationType capitalization;
            DateTimeOffset initialdate;
            DateTimeOffset expiration;

            Console.WriteLine("Please, enter the initial amount of money.");
            while (!double.TryParse(Console.ReadLine(), out initial) || !initial.IsPositive())
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }
            current = initial;
            Console.WriteLine("Please, enter the interest rate.");
            while (!double.TryParse(Console.ReadLine(), out rate) || !rate.IsPositive())
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }

            capitalization = (CapitalizationType) Deposit.ChooseCapitalizationType();

            Console.WriteLine("Please, enter the intial date of the deposit (yyyy-mm-dd).");
            while (!DateTimeOffset.TryParse(Console.ReadLine(), out initialdate))
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }

            Console.WriteLine("Please, enter the expiration date (yyyy-mm-dd).");
            while (!DateTimeOffset.TryParse(Console.ReadLine(), out expiration) || expiration.CompareTo(initialdate) < 0)
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }

            var diff = DateTimeOffset.UtcNow - initialdate;

            if (capitalization == CapitalizationType.No)
            {
                current = initial + (initial * rate * diff.Days / (365 * 100));
            }
            else
            if (capitalization == CapitalizationType.Monthly)
            {
                double percent = 1 + (rate * 30 / (100 * 365));
                current = initial * Math.Pow(percent, diff.Days / 30);
            }
            else
            {
                double percent = 1 + (rate * 365 / (100 * 365));
                current = initial * Math.Pow(percent, diff.Days / 365);
            }

            return new Deposit(id, initial, current, rate,capitalization,initialdate,expiration);

        }

        public static int ChooseCapitalizationType()
        {
            int cat = 0;
            Console.WriteLine("Choose the category:");
            Console.WriteLine($"1. {CapitalizationType.No}.");
            Console.WriteLine($"2. {CapitalizationType.Monthly}.");
            Console.WriteLine($"3. {CapitalizationType.Yearly}.");
            while (!int.TryParse(Console.ReadLine(), out cat) || cat < 1 || cat > 3)
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }
            return cat;
        }

        public override string ToString()
        {
            string str;
            str = $"{Id};{InitialMoney};{CurrentMoney};{InterestRate};{(int)Capitalization};{InitialDate};{ExpirationDate}";
            return str;
        }
    }
}
