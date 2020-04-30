using System;

namespace FinancialAssistant
{
    public class Credit
    {
        public int Id { get; set; }
        public double TotalMoneyAmount { get; set;}
        public double RemainingAmount { get; set; }
        public double InterestRate { get; set; }
        public DateTimeOffset LoanDate { get; set; }
        public DateTimeOffset RepaymentDate { get; set; }

        public Credit(int id, double total,double remain, double rate, DateTimeOffset loan,DateTimeOffset repayment)
        {
            Logger.Log.Info("Credit constructor was called");

            TotalMoneyAmount = total;
            RemainingAmount = remain;
            InterestRate = rate;
            LoanDate = loan;
            RepaymentDate = repayment;
            Id = id;
        }

        public static Credit Create(int id)
        { 
            double total = 0;
            double remain = 0;
            double rate = 0;
            DateTimeOffset loan;
            DateTimeOffset repayment;

            Console.WriteLine("Please, enter the credit amount.");
            while(!double.TryParse(Console.ReadLine(),out total) || !total.IsPositive() )
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }

            Console.WriteLine("Please, enter the interest rate.");
            while (!double.TryParse(Console.ReadLine(), out rate) || !rate.IsPositive())
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }

            Console.WriteLine("Please, enter the loan date (yyyy-mm-dd).");
            while (!DateTimeOffset.TryParse(Console.ReadLine(), out loan))
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }

            Console.WriteLine("Please, enter the repayment date (yyyy-mm-dd).");
            while (!DateTimeOffset.TryParse(Console.ReadLine(), out repayment) || repayment.CompareTo(loan)<0)
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }

            total = total * (1 + rate / 100);
            remain = total;

            Logger.Log.Info("New credit was created");
            return new Credit( id, total, remain, rate, loan, repayment);

        }

        public override string ToString()
        {
            string str;
            str = $"{Id};{TotalMoneyAmount};{RemainingAmount};{InterestRate};{LoanDate};{RepaymentDate}";
            return str;
        }
    }
}
