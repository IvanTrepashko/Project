using System.Collections.Generic;

namespace FinancialAssistant
{
    public interface IRepository<T>
    {
        void Add(T obj);
        List<T> GetAll();
        void ShowAll();
        void WriteToFile();
        void Delete();
    }
}
