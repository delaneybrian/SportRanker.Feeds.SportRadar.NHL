using System.Threading.Tasks;

namespace SportRanker.Feeds.SportRadar.NHL.Interfaces
{
    public interface IFeedProcessor
    {
        Task StartProcessing();
    }
}
