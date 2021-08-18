using System;
using System.Threading.Tasks;
using CoinJar.BLL.DataContract;
using CoinJar.Data;
using Microsoft.Extensions.Configuration;

namespace CoinJar.BLL
{
    public static class CoinJarBLL
    {
        public static void InitialiseConnectionString(IConfiguration configuration)
        {
            try
            {
                if (configuration != null)
                {
                    if (string.IsNullOrWhiteSpace(Utilities.DatabaseHelper.ConnectionString))
                    {
                        Utilities.DatabaseHelper.ConnectionString = configuration.GetConnectionString("DatabasePath");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static class CoinHelper
        {
            public static async Task<CoinResp> AddCoin(AddCoinReq addCoinReq)
            {
                try
                {
                    return await new Class.CoinJar().AddCoin(new Class.Coin()
                    {
                        Amount = addCoinReq.Amount,
                        Volume = addCoinReq.Volume
                    });
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static async Task<TotalAmountResp> GetTotalAmount()
            {
                try
                {
                    return await new Class.CoinJar().GetTotalAmount();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static async Task Reset()
            {
                try
                {
                    await new Class.CoinJar().Reset();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
