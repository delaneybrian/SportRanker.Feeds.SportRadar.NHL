using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using SportRanker.Feeds.SportRadar.NHL.Interfaces;
using Autofac;

namespace SportRanker.Feeds.SportRadar.NHL.App
{
    public static class NHLHistoricalResults
    {
        private static readonly string DaysQueryParam = "days";

        [FunctionName("NHLHistoricalResults")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            var queryParams = req.GetQueryParameterDictionary();

            if (queryParams.TryGetValue(DaysQueryParam, out var days))
            {
                if (int.TryParse(days, out var daysNum))
                {
                    log.Info("Request recieved attempting to process historical data");

                    var container = AppBootstrapper.Bootstrap();

                    var feedProcessor = container.Resolve<IFeedProcessor>();

                    feedProcessor.ProcessHistoricalFixtures(daysNum).Wait();

                    return new OkResult();
                }
            }

            return new BadRequestResult();
        }
    }
}
