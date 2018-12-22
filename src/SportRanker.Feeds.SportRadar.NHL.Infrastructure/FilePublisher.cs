using SportRanker.Contracts.SystemEvents;
using SportRanker.Feeds.SportRadar.NHL.Interfaces;
using Newtonsoft.Json;
using System.IO;

namespace SportRanker.Feeds.SportRadar.NHL.Infrastructure
{
    public class FilePublisher : IPublisher
    {
        private string fileLocation = @"C:/Fixtures/nhl.txt";

        public void PublishFixtureResult(FixtureResult fixtureResult)
        {
            var json = JsonConvert.SerializeObject(fixtureResult);

            using (StreamWriter writer = File.AppendText(fileLocation))
            {
                writer.WriteLine(json);
            }
        }
    }
}
