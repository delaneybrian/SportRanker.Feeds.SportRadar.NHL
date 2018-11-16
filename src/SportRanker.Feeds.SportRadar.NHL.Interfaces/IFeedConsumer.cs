using SportRanker.Feeds.SportRadar.NHL.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportRanker.Feeds.SportRadar.NHL.Interfaces
{
    public interface IFeedConsumer
    {
        Task<ICollection<FeedFixture>> GetFixtureResultsForYesterdayAsync();
        Task<ICollection<FeedFixture>> GetFixtureResultsForPreviousDaysAsync(int numDays);
    }
}
