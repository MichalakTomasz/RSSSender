using RSSSender.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSSSender.Services
{
    public interface IRssStoreService
    {
        Task SaveRssDataAsync(RssData rssData);
        Task<IEnumerable<RssData>> GetItemsAsync(string query);
        Task<IEnumerable<RssData>> GetAllItemsAsync();
    }
}