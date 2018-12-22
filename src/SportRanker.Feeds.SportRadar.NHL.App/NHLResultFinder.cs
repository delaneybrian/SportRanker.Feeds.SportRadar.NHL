using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Autofac;
using SportRanker.Feeds.SportRadar.NHL.Interfaces;

namespace SportRanker.Feeds.SportRadar.NHL.App
{
    public static class NHLResultFinder
    {
        [FunctionName("NHLResultFinder")]
        public static void Run([TimerTrigger("0 0 09 * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C#" +
               $" Timer trigger function executed at: {DateTime.Now}");

            var container = AppBootstrapper.Bootstrap();

            var feedProcessor = container.Resolve<IFeedProcessor>();

            feedProcessor.StartProcessing().Wait();
        }
    }
}
