using System.Threading.Tasks;
using CoinJar.BLL.DataContract;

namespace CoinJar.BLL.Interface
{
    public interface ICoinJar
    {
        Task<CoinResp> AddCoin(ICoin coin);
        Task<TotalAmountResp> GetTotalAmount();
        Task Reset();
    }
}
