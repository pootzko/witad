public class AnomalyDetectionModel
{
    public string StdOut { get; set; }

    public string Plot { get; set; }

    public IEnumerable<IEnumerable<string>> AnomalyValues { get; set; }

    public AnomalyDetectionErrorModel Error { get; set; }
}

public class AnomalyDetectionResponseModel
{
    public AnomalyDetectionResultsModel Results { get; set; }

    public AnomalyDetectionErrorModel Error { get; set; }
}

public class AnomalyDetectionResultsModel
{
    public AnomalyDetectionResultModel AnomaliesPlot { get; set; }

    public AnomalyDetectionResultModel Anomalies { get; set; }
}

public class AnomalyDetectionResultModel
{
    public string Type { get; set; }

    public AnomalyDetectionResultValueModel Value { get; set; }
}

public class AnomalyDetectionResultValueModel
{
    public IEnumerable<string> ColumnNames { get; set; }

    public IEnumerable<string> ColumnTypes { get; set; }

    public IEnumerable<IEnumerable<string>> Values { get; set; }
}

public class AnomalyDetectionErrorModel
{
    public string Code { get; set; }

    public string Message { get; set; }

    public IEnumerable<AnomalyDetectionErrorDetailModel> Details { get; set; }
}

public class AnomalyDetectionErrorDetailModel
{
    public string Code { get; set; }

    public string Target { get; set; }

    public string Message { get; set; }
}