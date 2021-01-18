using System;
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
            var email = new MailSender(new MailSenderConfigString(Environment.GetEnvironmentVariable("YourDailyReportMailConfig")));
            
            await email.Send("silas.reinagel@gmail.com", "Test Report 2 - 01/15/21", "Test Data\r\nLine 2");
        }

        [TestMethod]
        public async Task CoinApi_GetCurrentEthPrice()
        {
            var api = new CoinApi();

            var price = await api.GetSymbolUsdPrice("ETH");
            
            Assert.IsTrue(price > 0);
        }
    }
}
