using System;
using System.Collections.Generic;

#nullable disable

namespace CoinJar.Data.Entities
{
    public partial class Coin
    {
        public Coin()
        {
            CoinItems = new HashSet<CoinItem>();
        }

        public Guid CoinId { get; set; }
        public decimal Amount { get; set; }

        public virtual ICollection<CoinItem> CoinItems { get; set; }
    }
}
