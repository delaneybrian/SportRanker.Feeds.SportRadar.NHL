﻿using Autofac;
using SportRanker.Feeds.SportRadar.NHL.Application;
using SportRanker.Feeds.SportRadar.NHL.Infrastructure;
using SportRanker.Feeds.SportRadar.NHL.Interfaces;

namespace SportRanker.Feeds.SportRadar.NHL.App
{
    public class AppBootstrapper
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
                .RegisterType<Publisher>()
                .As<IPublisher>();

            return builder.Build();
        }
    }
}
