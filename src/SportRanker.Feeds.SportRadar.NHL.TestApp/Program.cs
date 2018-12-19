using Autofac;
using SportRanker.Feeds.SportRadar.NHL.Interfaces;

namespace SportRanker.Feeds.SportRadar.NHL.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = AppBootstrapper.Bootstrap();

            var feedProcessor = container.Resolve<IFeedProcessor>();

            feedProcessor.ProcessHistoricalFixtures(7).Wait();
        }
    }
}
