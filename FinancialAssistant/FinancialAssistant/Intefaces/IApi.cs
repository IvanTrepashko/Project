using System.Threading.Tasks;


namespace FinancialAssistant
{
    public interface IApi<T>
    {
        Task<T> Load();
    }
}
