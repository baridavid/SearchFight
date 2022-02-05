using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISearchClientAsync
    {
        Task<long> GetResultsByEngineAsync(string language);
        string EngineName { get; }
    }
}
