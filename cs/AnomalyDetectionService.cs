public class AnomalyDetectionService : IAnomalyDetectionService
{
    private readonly IAnomalyDetectionProvider _anomalyDetectionProvider;

    public AnomalyDetectionService(IAnomalyDetectionProvider anomalyDetectionProvider)
    {
        _anomalyDetectionProvider = anomalyDetectionProvider;
    }

    public async Task<AnomalyDetectionModel> DetectAnomaliesAsync(AnomalyDetectionFilter filter)
    {
        var response = await _anomalyDetectionProvider.DetectAnomaliesAsync(filter);
        var serializedPlotValues = response.Results?.AnomaliesPlot.Value.Values.FirstOrDefault()?.FirstOrDefault();
        var plotValues = JsonConvert.DeserializeObject<IDictionary<string, object>>(serializedPlotValues);
        var stdOut = plotValues?["Standard Output"].ToString();
        var plot = (plotValues?["Graphics Device"] as JArray)?.ToList().FirstOrDefault()?.ToString();
        var anomalies = response.Results?.Anomalies.Value.Values;

        var anomalyModel = new AnomalyDetectionModel
        {
            StdOut = stdOut,
            Plot = plot,
            AnomalyValues = anomalies,
            Error = response.Error
        };

        return anomalyModel;
    }
}