﻿using SportRanker.Feeds.SportRadar.NHL.Interfaces;
using System.Threading.Tasks;
using SportRanker.Feeds.SportRadar.NHL.Application.Extensions;

namespace SportRanker.Feeds.SportRadar.NHL.Application
{
    public class FeedProcessor : IFeedProcessor
    {
        public readonly IFeedConsumer _feedConsumer;
        public readonly IPublisher _publisher;
        public readonly IFixtureResultDeriver _fixtureResultDeriver;

        public FeedProcessor(IFeedConsumer feedConsumer,
            IPublisher publisher,
            IFixtureResultDeriver fixtureResultDeriver)
        {
            _feedConsumer = feedConsumer;
            _publisher = publisher;
            _fixtureResultDeriver = fixtureResultDeriver;
        }

        public async Task ProcessHistoricalFixtures(int days)
        {
            var feedResults = await _feedConsumer.GetFixtureResultsForPreviousDaysAsync(days);

            foreach (var feedResult in feedResults)
            {
                var fixtureResultMaybe = await _fixtureResultDeriver.TryGenerateFixtureResult(feedResult);

                if (fixtureResultMaybe.TrySome(out var fixtureResult))
                    _publisher.PublishFixtureResult(fixtureResult);
            }
        }

        public async Task StartProcessing()
        {
            var feedResults = await _feedConsumer.GetFixtureResultsForPreviousDaysAsync(8);

            foreach (var feedResult in feedResults)
            {
                var fixtureResultMaybe = await _fixtureResultDeriver.TryGenerateFixtureResult(feedResult);

                if (fixtureResultMaybe.TrySome(out var fixtureResult))
                    _publisher.PublishFixtureResult(fixtureResult);
            }
        }
    }
}
