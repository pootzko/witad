[RoutePrefix("api/v1/anomalies")]
public class AnomaliesController
{
    private readonly IAnomalyDetectionService _anomalyDetectionService;

    public AnomaliesController(IAnomalyDetectionService anomalyDetectionService)
    {
        _anomalyDetectionService = anomalyDetectionService;
    }

    [HttpPost]
    [Route("detect")]
    public async Task<IHttpActionResult> DetectAsync(AnomalyDetectionFilter model)
    {
        var returnModel = await _anomalyDetectionService.DetectAnomaliesAsync(model);

        return Ok(returnModel);
    }
}