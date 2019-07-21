public class AnomalyDetectionRequestBody
{
    public object Inputs { get; }

    public IDictionary<string, string> GlobalParameters { get; }

    public AnomalyDetectionRequestBody(string influxDataUrl, AnomalyDetectionFilter filter)
    {
        Inputs = new
        {
            InfluxDataUrl = new
            {
                ColumnNames = new [] { "Col1" },
                Values = new[] { new [] { influxDataUrl } }
            }
        };
        GlobalParameters = new Dictionary<string, string>
        {
            { "Add expected value column", filter.AddExpectedValue.ToString() },
            { "Remove NAs", filter.RemoveNAs.ToString() },
            { "Verbose", filter.Verbose.ToString() },
            { "Plot title", filter.PlotTitle ?? "" },
            { "Y-axis label", filter.YLabel ?? "" },
            { "X-axis label", filter.XLabel ?? "" },
            { "Apply log scaling?", filter.ApplyYLogScaling.ToString() },
            { "Create plot?", filter.CreatePlot.ToString() },
            { "Piecewise median time window", filter.PiecewiseMedianPeriodWeeks.ToString() },
            { "Longterm time-series", filter.IsLongtermTimeseries.ToString() },
            { "Threshold", filter.Threshold ?? "" },
            { "Only last day?", filter.OnlyLastDay ?? "" },
            { "Alpha significance", filter.AlphaSignificance.ToString() },
            { "Direction", filter.Direction ?? "" },
            { "Max anomalies to return", filter.MaxAnomalies.ToString() }
        };
    }
}