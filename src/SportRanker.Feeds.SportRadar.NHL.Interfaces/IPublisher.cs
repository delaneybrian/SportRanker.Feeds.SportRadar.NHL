using SportRanker.Contracts.SystemEvents;

namespace SportRanker.Feeds.SportRadar.NHL.Interfaces
{
    public interface IPublisher
    {
        void PublishFixtureResult(FixtureResult fixtureResult);
    }
}
