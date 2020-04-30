using System.Threading.Tasks;

namespace FinancialAssistant
{
    public interface IApiRepository<T>
    {
        Task CreateRepository();
        void ShowAll();
        T GetById(int id);
    }
}
