using Google.Apis.AnalyticsReporting.v4.Data;
using INGLife.Event.Models.GoogleApisModels;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace INGLife.Event.Infrastructures.GoogleAnalyticsReportingServices {
    public class GoogleAnalyticsReportingService: IGoogleAnalyticsReportingService {
        private string viewId = ConfigurationManager.AppSettings["google.apis.viewId"] as string;
        public List<GoogleApisAnalyticsReportingResponseModel> GetData(GoogleApisAnalyticsReportingRequestModel model) {

            var responseModel = new List<GoogleApisAnalyticsReportingResponseModel>();
            try {
                using (var analyticsreporting = GoogleServiceAccount.AuthenticateServiceAccountFromKey()) {
                    //set parameters
                    var dateRange = new DateRange() { StartDate = model.StartDate.ToString("yyyy-MM-dd"), EndDate = model.EndDate.ToString("yyyy-MM-dd") };
                    var dimensions = new List<Dimension>() {
                        new Dimension { Name = "ga:date" },
                        new Dimension { Name = "ga:pagePath" }
                    };
                    var metrics = new List<Metric>() {
                        new Metric { Expression = "ga:pageviews", Alias = "PV" },
                        new Metric { Expression = "ga:uniquePageviews", Alias = "UV" }
                    };

                    var filters = "";
                    if (!string.IsNullOrEmpty(model.PagePath)) {
                        filters = string.Format("ga:pagePath=={0}", model.PagePath);
                    }

                    var orderBys = new List<OrderBy>() {
                        new OrderBy { FieldName = "ga:date", SortOrder = "descending" }
                    };

                    //request report
                    var reportRequest = new ReportRequest {
                        ViewId = viewId,
                        DateRanges = new List<DateRange>() { dateRange },
                        Dimensions = dimensions,
                        Metrics = metrics,
                        FiltersExpression = filters,
                        //Segments = segments,
                        OrderBys = orderBys
                    };

                    //result
                    var requests = new List<ReportRequest>();
                    requests.Add(reportRequest);

                    // Create the GetReportsRequest object.
                    var getReport = new GetReportsRequest() { ReportRequests = requests };

                    // Call the batchGet method.
                    var response = analyticsreporting.Reports.BatchGet(getReport).Execute();

                    //mapper viewModels
                    foreach (Report report in response.Reports) {
                        var rows = (List<ReportRow>)report.Data.Rows;
                        if (rows == null) {
                            throw new Exception("데이터가 없습니다.");
                        }
                        foreach (ReportRow row in rows) {
                            responseModel.Add(new GoogleApisAnalyticsReportingResponseModel {
                                Date = row.Dimensions[0],
                                PagePath = row.Dimensions[1],
                                PageViews = row.Metrics[0].Values[0],
                                UniquePageviews = row.Metrics[0].Values[1]
                            });
                        }
                    }
                }
            } catch (Exception e) {

            }

            return responseModel;
        }
    }
}