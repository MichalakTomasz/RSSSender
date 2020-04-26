using Microsoft.Azure.Cosmos;
using RSSSender.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSSSender.Services
{
    public class CosmosDBRSSStoreServie : IRssStoreService
    {
        private readonly Container container;
        public CosmosDBRSSStoreServie(
            CosmosClient cosmosClient,
            string dbName,
            string containerName)
            => container = cosmosClient.GetContainer(dbName, containerName);

        public async Task<IEnumerable<RssData>> GetItemsAsync(string queryString)
        {
            var query = container.GetItemQueryIterator<RssData>(
                new QueryDefinition(queryString));
            var result = new List<RssData>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response.ToList());
            }
            return result;
        }

        public async Task<IEnumerable<RssData>> GetAllItemsAsync()
            => await GetItemsAsync("select * from RssTable");

        public async Task SaveRssDataAsync(RssData rssData)
        {
            await container.CreateItemAsync<RssData>(
                rssData, new PartitionKey(rssData.ID));
        }
    }
}
