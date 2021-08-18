using System.Threading.Tasks;
using CoinJar.BLL.DataContract;
using CoinJar.Data;
using NUnit.Framework;

namespace CoinJar.BLL.UnitTest
{
    public class GetTotalAmountUnitTest
    {
        [SetUp]
        public void Setup()
        {
            Utilities.DatabaseHelper.ConnectionString = UnitTestHelper.ConnectionString;
        }

        [Test]
        public async Task GetTotalAmount()
        {
            TotalAmountResp _totalAmountResp = await CoinJarBLL.CoinHelper.GetTotalAmount();

            Assert.AreEqual(0.275M, _totalAmountResp.GrandTotalAmount);
        }
    }
}