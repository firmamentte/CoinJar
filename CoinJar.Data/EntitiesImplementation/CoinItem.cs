using System;

namespace CoinJar.Data.Entities
{
    public partial class CoinItem
    {
        public decimal TotalAmount
        {
            get
            {
                return Math.Round(Coin.AmountInDollar * Volume, 3);
            }
        }

        public decimal Amount
        {
            get
            {
                return Coin.Amount;
            }
        }
    }
}
