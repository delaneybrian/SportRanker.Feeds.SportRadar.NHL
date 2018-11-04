using SportRanker.Feeds.SportRadar.NHL.Contracts;

namespace SportRanker.Feeds.SportRadar.NHL.Interfaces
{
    public interface IPublisher
    {
        void PublishFixtureResult(FixtureResult fixtureResult);
    }
}
