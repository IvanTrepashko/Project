using NbrbAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace FinancialAssistant
{
    public interface IApi<T>
    {
        Task<T> Load();
    }
}
