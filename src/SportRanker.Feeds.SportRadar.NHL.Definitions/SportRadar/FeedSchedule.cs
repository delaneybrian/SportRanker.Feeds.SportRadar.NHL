using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SportRanker.Feeds.SportRadar.NHL.Definitions
{
    public class FeedSchedule
    {
        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "league")]
        public League League { get; set; }

        [DataMember(Name = "games")]
        public ICollection<Game> Games { get; set; }
    }
}
