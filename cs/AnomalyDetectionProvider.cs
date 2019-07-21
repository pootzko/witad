public class AnomalyDetectionProvider : IAnomalyDetectionProvider
{
    public async Task<AnomalyDetectionResponseModel> DetectAnomaliesAsync(AnomalyDetectionFilter filter)
    {
        var requestUrl = AppSettings.AnomaliesServiceUrl;
        var content = new AnomalyDetectionRequestBody(GetAnomalyDetectionDataFetchUrl(filter), filter);
        var serializedContent =  JsonConvert.SerializeObject(content);
        var headers = new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {AppSettings.AnomaliesAPIKey}" },
            { "Accept", "application/json" }
        };

        var responseContent = await PostAsync(requestUrl, serializedContent, headers);
        if (String.IsNullOrEmpty(responseContent))
        {
            return null;
        }

        var anomalyDetectionModel = responseContent.FromJsonString<AnomalyDetectionResponseModel>();

        return anomalyDetectionModel;
    }

    private string GetAnomalyDetectionDataFetchUrl(AnomalyDetectionFilter filter)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "u", AppSettings.InfluxDbAnomalyUsername },
            { "p", AppSettings.InfluxDbAnomalyPassword },
            { "db", AppSettings.InfluxDbName },
            { "q", GetAnomalyDetectionQuery(filter) }
        };

        var serializedQueryParams = String.Join("&", queryParams.Select(p => $"{p.Key}={WebUtility.UrlEncode(p.Value)}"));

        return $"{AppSettings.InfluxDbUri}query?{serializedQueryParams}";
    }

    public string GetAnomalyDetectionQuery(AnomalyDetectionFilter filter)
    {
        return $"select time, {filter.Metric} as value " +
                $"from {filter.Series} " +
                $"where (SensorId = '{filter.SensorId}') " +
                $"and (time >= '{filter.TimeFrom}' and time < '{filter.TimeTo}')";
    }

    private async Task<string> PostAsync(string requestUrl, string content, IDictionary<string, string> headers = null)
    {
        using (var httpClient = new HttpClient())
        {
            foreach (var header in headers)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(requestUrl, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Handle failure as you see fit");
            }

            return responseContent;
        }
    }
}