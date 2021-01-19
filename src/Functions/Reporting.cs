using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilasReinagel.YourDailyReport
{
    public sealed class Reporting
    {
        private readonly Email _email;
        private readonly CoinApi _coinApi;

        public Reporting(Email email, CoinApi coinApi)
        {
            _email = email;
            _coinApi = coinApi;
        }
        
        public async Task PublishAll(IEnumerable<ReportConfig> cfgs) 
            => await Task.WhenAll(cfgs.Select(Publish));

        public async Task Publish(ReportConfig cfg)
        {
            var report = await GenerateReport(cfg);
            await _email.Send(cfg.ToAddress, $"Your Daily Report - {DateTimeOffset.UtcNow.Date}", report);
        }
        
        private async Task<string> GenerateReport(ReportConfig cfg)
        {
            var report = new StringBuilder();
            report
                .AppendLine("Your Daily Report")
                .AppendLine("-----------------------")
                .AppendLine($"Generated at {DateTimeOffset.UtcNow}")
                .AppendLine("-----------------------");

            var elements = cfg.ReportElements;
            foreach (var e in elements)
            {
                var (key, value) = e;
                if (key.Equals("CryptoPriceUSD"))
                    report.AppendLine($"{key} - {value}: {Math.Round(await _coinApi.GetSymbolUsdPrice(value), 0)}");
                else
                    report.AppendLine($"Unknown Report Element: {key} - {value}");
            }
            
            return report.ToString();
        }
    }
}
