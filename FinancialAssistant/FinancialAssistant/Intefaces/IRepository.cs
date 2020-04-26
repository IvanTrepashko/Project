using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialAssistant
{
    public interface IRepository<T>
    {
        void Add(T obj);
        List<T> GetAll();
    }
}
