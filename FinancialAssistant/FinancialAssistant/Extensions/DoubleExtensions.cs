using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialAssistant
{
    public static class DoubleExtensions
    {
        public static bool IsPositive(this double x)
        {
            return x > 0;
        }
    }
}
