﻿using System;
using System.Globalization;



namespace NbrbAPI.Models
{
    public class Rate
    {
        private readonly CultureInfo _culture = CultureInfo.CreateSpecificCulture("be-BY");
        
        public int Cur_ID { get; set; }
        public string Cur_Abbreviation { get; set; }
        public int Cur_Scale { get; set; }
        public string Cur_Name { get; set; }
        public double Cur_OfficialRate { get; set; }

        public override string ToString()
        {
            string str;
            str = $"{Cur_Abbreviation} : {Cur_OfficialRate.ToString("C", _culture)}";
            return str;
        }

        public void ShowInformation()
        {
            Console.Write($"Currency name : {Cur_Name}.\n");
            Console.WriteLine($"{Cur_Scale} {Cur_Abbreviation} : {Cur_OfficialRate.ToString("C", _culture)}.");
        }
    }
}