# For details in input fields, check the heading of the ts_anom_detection.R file

AnomalyDetectionTsw <- function(
    input_dataset,
    max_anomalies = 0.10,
    direction = "pos",
    alpha_significance = 0.05,
    only_last_day = NULL,
    threshold = "None",
    add_expected_value = FALSE,
    is_longterm_timeseries = FALSE,
    piecewise_median_period_weeks = 2,
    create_plot = FALSE,
    apply_y_log_scaling = FALSE,
    x_label = "Time",
    y_label = "Value",
    plot_title = NULL,
    verbose = FALSE,
    remove_nas = FALSE
) {
    source("src/date_utils.R");
    source("src/detect_anoms.R");
    source("src/plot_utils.R");
    # source("src/vec_anom_detection.R");
    source("src/ts_anom_detection.R");

    if (nrow(input_dataset) == 0) {
        print("-----------")
        return (data.frame())
    }

    print("==========")

    if (only_last_day == "None") {
        only_last_day <- NULL
    }

    if (verbose) {
        print("Input Dataset")
        str(input_dataset)
    }

    input_dataset[[1]] <- as.POSIXlt(strptime(input_dataset[[1]], "%Y-%m-%dT%H:%M:%SZ"))

    if(verbose) {
        print("Input Dataset after time modifications")
        str(input_dataset)
    }

    result <- AnomalyDetectionTs(
        input_dataset,
        max_anomalies,
        direction,
        alpha_significance,
        only_last_day,
        threshold,
        add_expected_value,
        is_longterm_timeseries,
        piecewise_median_period_weeks,
        create_plot,
        apply_y_log_scaling,
        x_label,
        y_label,
        plot_title,
        verbose,
        remove_nas
    )

    if (verbose) {
        print("Results")
        str(result)
    }

    if (is.data.frame(result$anoms) && nrow(result$anoms) > 0) {
        result$anoms[[1]] <- as.character(result$anoms[[1]], format="%Y-%m-%dT%I:%M:%S %Z")

        if (create_plot == TRUE) {
            print(result$plot)
        }
    }

    return (result$anoms)
}
