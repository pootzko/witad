public class AnomalyDetectionFilter
{
    #region InfluxDB params

    /// <summary>
    /// Sensor id for which the request is for.
    /// </summary>
    public int SensorId { get; set; }

    /// <summary>
    /// Time from filter.
    /// Influx syntax expects the following format: "YYYY-MM-DD HH:mm:ss".
    /// </summary>
    [Required]
    public string TimeFrom { get; set; }

    /// <summary>
    /// Time to filter.
    /// Influx syntax expects the following format: "YYYY-MM-DD HH:mm:ss".
    /// </summary>
    [Required]
    public string TimeTo { get; set; }

    /// <summary>
    /// Density of series points.
    /// Can be something like: "5s", "1m", "1h"...
    /// </summary>
    [Required]
    public string Resolution { get; set; }

    /// <summary>
    /// Metric to fetch (Temperature, Humidity...)
    /// </summary>
    [Required]
    public string Metric { get; set; }

    /// <summary>
    /// Influx time series name from which to fetch.
    /// </summary>
    [Required]
    public string Series { get; set; }

    #endregion InfluxDB params

    #region Twitter anomaly detection params

    /// <summary>
    /// Maximum number of anomalies that S-H-ESD will detect as a percentage of the data (default is 0.10).
    /// Allowed: 0.00 - 0.999
    /// </summary>
    public float MaxAnomalies { get; set; }

    /// <summary>
    /// Directionality of the anomalies to be detected.
    /// Allowed: pos / neg / both
    /// </summary>
    public string Direction { get; set; }

    /// <summary>
    /// The level of statistical significance with which to accept or reject anomalies.
    /// Allowed: 0.01 - 0.1
    /// </summary>
    public float AlphaSignificance { get; set; }

    /// <summary>
    /// Find and report anomalies only within the last day or hr in the time series?
    /// Allowed: None / day / hr
    /// </summary>
    public string OnlyLastDay { get; set; }

    /// <summary>
    /// Only report positive going anomalies above the specified threshold.
    /// Allowed: None / med_max / p95 / p99
    /// </summary>
    public string Threshold { get; set; }

    /// <summary>
    /// Add an additional column to the anomalies output containing the expected value?
    /// </summary>
    public bool AddExpectedValue { get; set; }

    /// <summary>
    /// Increase anomaly detection efficacy for time-series that are greater than a month?
    /// This option should be set when the input time series is longer than a month. The option
    /// enables the approach described in Vallis, Hochenbaum, and Kejariwal (2014).
    /// </summary>
    public bool IsLongtermTimeseries { get; set; }

    /// <summary>
    /// The piecewise median time window as described in Vallis, Hochenbaum, and
    /// Kejariwal (2014). Defaults to 2.
    /// </summary>
    public int PiecewiseMedianPeriodWeeks { get; set; }

    /// <summary>
    /// A flag indicating whether a plot with both the time series and the estimated anomalies,
    /// indicated by circles, should also be returned.
    /// </summary>
    public bool CreatePlot { get; set; }

    /// <summary>
    /// Apply log scaling to the y-axis? This helps with viewing plots that have extremely
    /// large positive anomalies relative to the rest of the data.
    /// </summary>
    public bool ApplyYLogScaling { get; set; }

    /// <summary>
    /// X-axis label to be added to the output plot. Defaults to "Time".
    /// </summary>
    public string XLabel { get; set; }

    /// <summary>
    /// Y-axis label to be added to the output plot. Defaults to "Value".
    /// </summary>
    public string YLabel { get; set; }

    /// <summary>
    /// Title for the plot output.
    /// </summary>
    public string PlotTitle { get; set; }

    /// <summary>
    /// Enable debug messages?
    /// </summary>
    public bool Verbose { get; set; }

    /// <summary>
    /// Remove any NAs in timestamps?
    /// </summary>
    public bool RemoveNAs { get; set; }

    #endregion Twitter anomaly detection params
}