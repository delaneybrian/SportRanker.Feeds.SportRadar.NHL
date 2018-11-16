using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SportRanker.Contracts.Dto;
using SportRanker.Feeds.SportRadar.NHL.Definitions;
using SportRanker.Feeds.SportRadar.NHL.Interfaces;

namespace SportRanker.Feeds.SportRadar.NHL.Infrastructure
{
    public class FeedConsumer : IFeedConsumer
    {
        private readonly HttpClient _httpClient;

        private readonly string ApiKey = "t3m2u3dkb3mfdmujferxv9um";

        private readonly int DaysInYear = 365;

        public FeedConsumer()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ICollection<FeedFixture>> GetFixtureResultsForYesterdayAsync()
        {
            ICollection<FeedFixture> feedFixtures = new List<FeedFixture>();

            var yesterday = DateTime.UtcNow.AddDays(-1);

            var url = $"https://api.sportradar.us/nhl/trial/v6/en/games/{yesterday.Year}/{yesterday.Month}/{yesterday.Day}/schedule.json?api_key={ApiKey}";

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
                                FeedSource = SourceId.SportRadar,
                                ProviderFixtureId = game.Id,
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

        public async Task<ICollection<FeedFixture>> GetFixtureResultsForPreviousDaysAsync(int numDays)
        {
            ICollection<FeedFixture> feedFixtures = new List<FeedFixture>();

            if (numDays > DaysInYear)
                numDays = DaysInYear;

            if (numDays > 0)
                numDays = numDays * -1;

            while (numDays < 0)
            {
                var day = DateTime.UtcNow.AddDays(numDays);

                var url = $"https://api.sportradar.us/nhl/trial/v6/en/games/{day.Year}/{day.Month}/{day.Day}/schedule.json?api_key={ApiKey}";

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
                                    FeedSource = SourceId.SportRadar,
                                    ProviderFixtureId = game.Id,
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
                numDays += 1;
            }
            return feedFixtures;
        }
    }
}
