using RSSSender.Models;
using System.Threading.Tasks;

namespace RSSSender.Services
{
    public interface IRssStoreService
    {
        Task<RssData> GetRssDataAsync();
        Task SetRssDataAsync(RssData rssData);
    }
}