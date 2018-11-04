using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SportRanker.Feeds.SportRadar.NHL.Definitions;
using SportRanker.Feeds.SportRadar.NHL.Interfaces;

namespace SportRanker.Feeds.SportRadar.NHL.Infrastructure
{
    public class FeedConsumer : IFeedConsumer
    {
        private readonly HttpClient _httpClient;

        public FeedConsumer()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ICollection<FeedFixture>> GetFixtureResultsForYesterdayAsync()
        {
            ICollection<FeedFixture> feedFixtures = new List<FeedFixture>();

            var apiKey = "t3m2u3dkb3mfdmujferxv9um";

            var yesterday = DateTime.UtcNow.AddDays(-1);

            var url = $"https://api.sportradar.us/nhl/trial/v6/en/games/{yesterday.Year}/{yesterday.Month}/{yesterday.Day}/schedule.json?api_key={apiKey}";

            var result = await _httpClient.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };

                var feedSchedule = JsonConvert.DeserializeObject<FeedSchedule>(content, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });

                foreach (var game in feedSchedule.Games)
                {
                    if (game.Status == "closed")
                    {
                        feedFixtures.Add(
                            new FeedFixture()
                            {
                                KickOffTimeUtc = game.Scheduled,
                                HomeTeamId = game.Home.Id,
                                HomeTeamName = game.Home.Name,
                                HomeTeamScore = game.HomePoints,
                                AwayTeamId = game.Away.Id,
                                AwayTeamName = game.Away.Name,
                                AwayTeamScore = game.AwayPoints
                            });
                    }
                }
            }

            return feedFixtures;
        }
    }
}
