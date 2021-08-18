
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinJar.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CoinJar.Data
{
    public static class CoinJarDAL
    {
        public static async Task<Coin> GetCoinByAmount(CoinJarContext dbContext, decimal amount)
        {
            try
            {
                return await (from coin in dbContext.Coins.Cast<Coin>()
                              where coin.Amount == amount
                              select coin).
                              FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<SqlDataReader> GetTotalAmount(SqlConnection connection)
        {
            try
            {
                return await Utilities.DatabaseHelper.ExecuteSqlDataReader(connection, "spGetTotalAmount", null);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
