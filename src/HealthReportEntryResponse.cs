using System;
using System.Collections.Generic;

namespace Demo.AzFuncWithHealthChecks
{
    public class HealthReportEntryResponse
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public TimeSpan Duration { get; set; }
    }
}