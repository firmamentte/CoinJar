using System;
using System.Collections.Generic;

#nullable disable

namespace CoinJar.Data.Entities
{
    public partial class CoinItem
    {
        public Guid CoinItemId { get; set; }
        public Guid CoinId { get; set; }
        public decimal Volume { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeletionDate { get; set; }

        public virtual Coin Coin { get; set; }
    }
}
