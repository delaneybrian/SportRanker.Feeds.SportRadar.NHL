using System.Threading.Tasks;
using Optional;
using SportRanker.Contracts.SystemEvents;
using SportRanker.Feeds.SportRadar.NHL.Definitions;

namespace SportRanker.Feeds.SportRadar.NHL.Interfaces
{
    public interface IFixtureResultDeriver
    {
        Task<Option<FixtureResult>> TryGenerateFixtureResult(FeedFixture feedFixture);
    }
}
