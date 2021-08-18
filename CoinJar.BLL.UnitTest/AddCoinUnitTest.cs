using System.Threading.Tasks;
using CoinJar.BLL.DataContract;
using CoinJar.Data;
using NUnit.Framework;

namespace CoinJar.BLL.UnitTest
{
    public class AddCoinUnitTest
    {
        [SetUp]
        public void Setup()
        {
            Utilities.DatabaseHelper.ConnectionString = UnitTestHelper.ConnectionString;
        }

        [Test]
        public async Task AddCoin()
        {
            CoinResp _coinResp = await CoinJarBLL.CoinHelper.AddCoin(new()
            {
                Amount = 5,
                Volume = 0.5M
            });

            Assert.AreEqual(0.025M, _coinResp.TotalAmount);
        }
    }
}