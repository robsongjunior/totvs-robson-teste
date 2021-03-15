using System.Threading.Tasks;

namespace Robson_Totvs_Test.Configuration.TokenService
{
    public interface ITotvsTokenService
    {
        Task<string> GenerateTokenAsync(string username);
    }
}
