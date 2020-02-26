using RSSSender.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSSSender.Services
{
    public interface IRssStoreService
    {
        Task<IEnumerable<RssData>> LoadRssDataAsync();
        Task SaveRssDataAsync(RssData rssData);
    }
}