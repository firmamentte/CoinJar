namespace CoinJar.Data.Entities
{
    public partial class Coin
    {
        public decimal AmountInDollar
        {
            get
            {
                return Amount / 100;
            }
        }
    }
}
