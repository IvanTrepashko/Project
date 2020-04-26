using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialAssistant
{
    public static class IntExtensions
    {
        public static bool IsPositive(this int x)
        {
            return x > 0;
        }
    }
}
