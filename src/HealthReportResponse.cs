using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Demo.AzFuncWithHealthChecks
{
    public class HealthReportResponse
    {
        public HealthReportResponse(HealthReport report)
        {
            Status = report.Status.ToString();
            Results = new Dictionary<string, HealthReportEntryResponse>();
            TotalDuration = report.TotalDuration;

            report.Entries.ToList().ForEach(pair =>
            {
                var data = new Dictionary<string, object>();
                pair.Value.Data.ToList().ForEach(p =>
                {
                    data.Add(p.Key, p.Value);
                });

                var entry = new HealthReportEntryResponse
                {
                    Status = pair.Value.Status.ToString(),
                    Description = pair.Value.Description,
                    Data = data,
                    Duration = pair.Value.Duration
                };

                this.Results.Add(pair.Key, entry);
            });
        }

        public string Status { get; set; }
        public Dictionary<string, HealthReportEntryResponse> Results { get; set; }
        public TimeSpan TotalDuration { get; set; }
    }
}