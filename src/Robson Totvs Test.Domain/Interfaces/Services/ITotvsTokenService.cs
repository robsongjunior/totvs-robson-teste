using System.Threading.Tasks;

namespace Robson_Totvs_Test.Domain.Interfaces.Services
{
    public interface ITotvsTokenService
    {
        Task<string> GenerateTokenAsync(string username);
    }
}
