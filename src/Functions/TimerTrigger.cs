using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SilasReinagel.YourDailyReport
{
    public static class TimerTrigger
    {
        private static ReportConfig _staticReportConfig = new ReportConfig
        {
            ToAddress = "silas.reinagel@gmail.com",
            ReportElements = new ReportElements
            {
                ("CryptoPriceUSD", "BTC"),
                ("CryptoPriceUSD", "ETH")
            }
        };
        
        [FunctionName("TimerTrigger")]
        public static async Task Run([TimerTrigger("0 0 11 * * *")]TimerInfo myTimer, ILogger log)
        {
            var reporting = new Reporting(new Email(), new CoinApi(), log);
            
            await reporting.Publish(_staticReportConfig);
        }
    }
}
