using System.Collections.Generic;

namespace CoinJar.BLL.DataContract
{
   public class TotalAmountResp
    {
        public List<CoinResp> Coins { get; set; } = new();
        public decimal GrandTotalAmount { get; set; }
        public decimal GrandTotalCount { get; set; }
    }
}
