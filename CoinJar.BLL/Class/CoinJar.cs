using System;
using System.Linq;
using System.Threading.Tasks;
using CoinJar.BLL.DataContract;
using CoinJar.BLL.Interface;
using CoinJar.Data;
using Microsoft.Data.SqlClient;

namespace CoinJar.BLL.Class
{
    public class CoinJar : ICoinJar
    {
        public async Task<CoinResp> AddCoin(ICoin coin)
        {
            try
            {
                using Data.Entities.CoinJarContext _dbContext = new();

                Data.Entities.CoinItem _coinItem = new()
                {
                    Coin = await CoinJarDAL.GetCoinByAmount(_dbContext, coin.Amount),
                    Volume = coin.Volume,
                    CreationDate = DateTime.Now.Date,
                    DeletionDate = Utilities.DateHelper.DefaultDate
                };

                await _dbContext.CoinItems.AddAsync(_coinItem);
                await _dbContext.SaveChangesAsync();

                return new CoinResp()
                {
                    Amount = _coinItem.Amount,
                    TotalAmount = _coinItem.TotalAmount,
                    TotalCount = _coinItem.Volume
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TotalAmountResp> GetTotalAmount()
        {
            try
            {
                using SqlConnection _connection = new(Utilities.DatabaseHelper.ConnectionString);

                using var _sqlDataReader = await CoinJarDAL.GetTotalAmount(_connection);

                TotalAmountResp _totalAmountResp = new();

                if (_sqlDataReader.HasRows)
                {
                    while (await _sqlDataReader.ReadAsync())
                    {
                        _totalAmountResp.Coins.Add(new CoinResp()
                        {
                            Amount = Convert.ToDecimal(_sqlDataReader["Amount"]),
                            TotalAmount = Math.Round(Convert.ToDecimal(_sqlDataReader["TotalAmount"]), 3),
                            TotalCount = Math.Round(Convert.ToDecimal(_sqlDataReader["TotalCount"]), 3)
                        });
                    }

                    _totalAmountResp.GrandTotalAmount = _totalAmountResp.Coins.Sum(totalAmount => totalAmount.TotalAmount);
                    _totalAmountResp.GrandTotalCount = _totalAmountResp.Coins.Sum(totalAmount => totalAmount.TotalCount);
                }

                return _totalAmountResp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Reset()
        {
            try
            {
                using SqlConnection _connection = new(Utilities.DatabaseHelper.ConnectionString);

                await Utilities.DatabaseHelper.ExecuteNonQuery(_connection, "spResetCoinItem", null);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
