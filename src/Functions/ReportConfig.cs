using System;
using System.Collections.Generic;

namespace SilasReinagel.YourDailyReport
{
    public sealed class ReportConfig
    {
        public string ToAddress { get; set; }
        public ReportElements ReportElements { get; set; }
    }
    
    public sealed class ReportElements : List<(string, string)>{}
}
