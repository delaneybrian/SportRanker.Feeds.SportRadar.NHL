using System.Threading.Tasks;

namespace SportRanker.Feeds.SportRadar.NHL.Interfaces
{
    public interface IFeedProcessor
    {
        Task StartProcessing();
        Task ProcessHistoricalFixtures(int days);
    }
}
