using System;
using System.Collections.Generic;
using System.IO;


namespace FinancialAssistant
{
    public static class Hints
    {
        private static List<string> hints = new List<string>();
        private static readonly string _path = @"..\..\..\hints.txt";

        static Hints()
        {
            var data = File.ReadAllLines(_path);

            foreach (var item in data)
            {
                hints.Add(item);
            }
        }

        public static void GetRandomHint()
        {
            Random random = new Random();
            Console.WriteLine("Random hint : ");
            Console.WriteLine(hints[random.Next(0, hints.Count - 1)]);
        }
    }
}
