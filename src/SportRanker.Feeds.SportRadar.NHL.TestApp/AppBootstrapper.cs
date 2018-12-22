using Autofac;
using SportRanker.Feeds.SportRadar.NHL.Application;
using SportRanker.Feeds.SportRadar.NHL.Infrastructure;
using SportRanker.Feeds.SportRadar.NHL.Interfaces;

namespace SportRanker.Feeds.SportRadar.NHL.TestApp
{
    public static class AppBootstrapper
    {
        public static IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<FeedProcessor>()
                .As<IFeedProcessor>();

            builder
                .RegisterType<FeedConsumer>()
                .As<IFeedConsumer>();

            builder
                .RegisterType<FilePublisher>()
                .As<IPublisher>();

            builder
                .RegisterType<FixtureResultDeriver>()
                .As<IFixtureResultDeriver>();

            builder
                .RegisterType<DataProvider>()
                .As<IDataProvider>();

            return builder.Build();
        }
    }
}
