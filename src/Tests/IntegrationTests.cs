using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilasReinagel.YourDailyReport;

namespace Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public async Task Email_TestSend()
        {
            var email = new Email();
            
            await email.Send("silas.reinagel@gmail.com", "Test Report 2 - 01/15/21", "Test Data\r\nLine 2");
        }

        [TestMethod]
        public async Task CoinApi_GetCurrentEthPrice()
        {
            var api = new CoinApi();

            var price = await api.GetSymbolUsdPrice("ETH");
            
            Assert.IsTrue(price > 0);
        }

        [TestMethod]
        public async Task Reporting_SendDailyReport()
        {
            var reporting = new Reporting(new Email(), new CoinApi());
            
            await reporting.Publish(new ReportConfig
            {
                ToAddress = "silas.reinagel@gmail.com", 
                ReportElements = new ReportElements
                {
                    ("CryptoPriceUSD", "BTC"),
                    ("CryptoPriceUSD", "ETH"),
                }
            });
        }
    }
}
