using System;
using CoinJar.BLL.Interface;

namespace CoinJar.BLL.Class
{
    public class Coin : ICoin
    {
        public decimal Amount { get; set; }
        public decimal Volume { get; set; }
    }
}
