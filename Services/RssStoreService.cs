using RSSSender.Models;
using System.Threading.Tasks;

namespace RSSSender.Services
{
    public class RssStoreService : IRssStoreService
    {
        public async Task SetRssDataAsync(RssData rssData)
            => await Task.Run(() => { });

        public async Task<RssData> GetRssDataAsync()
             => await Task.Run(() => { return new RssData(); });
    }
}
