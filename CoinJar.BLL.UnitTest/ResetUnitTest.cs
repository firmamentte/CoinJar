using System.Threading.Tasks;
using CoinJar.BLL.DataContract;
using CoinJar.Data;
using NUnit.Framework;

namespace CoinJar.BLL.UnitTest
{
    public class ResetUnitTest
    {
        [SetUp]
        public void Setup()
        {
            Utilities.DatabaseHelper.ConnectionString = UnitTestHelper.ConnectionString;
        }

        [Test]
        public async Task Reset()
        {
            await CoinJarBLL.CoinHelper.Reset();

            TotalAmountResp _totalAmountResp = await CoinJarBLL.CoinHelper.GetTotalAmount();

            Assert.AreEqual(0M, _totalAmountResp.GrandTotalAmount);
        }
    }
}