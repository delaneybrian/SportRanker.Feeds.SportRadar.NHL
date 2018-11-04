using SportRanker.Feeds.SportRadar.NHL.Contracts;
using SportRanker.Feeds.SportRadar.NHL.Definitions;

namespace SportRanker.Feeds.SportRadar.NHL.Application
{
    public static class ResultConverter
    {
        public static FixtureResult ConvertFromFeedFixtureResult(FeedFixture feedFixture)
        {
            return new FixtureResult()
            {
                SportId = SportId.NBA,
                FeedSource = FeedSource.SportRadar,
                HomeTeamId = feedFixture.HomeTeamId,
                HomeTeamName = feedFixture.HomeTeamName,
                HomeTeamScore = feedFixture.HomeTeamScore,
                AwayTeamId = feedFixture.AwayTeamId,
                AwayTeamName = feedFixture.AwayTeamName,
                AwayTeamScore = feedFixture.AwayTeamScore,
                KickOffTimeUtc = feedFixture.KickOffTimeUtc
            };
        }
    }
}
