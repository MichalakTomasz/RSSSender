using Microsoft.Extensions.Configuration;
using RSSSender.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RSSSender.Services
{
    public class RssStoreService : IRssStoreService
    {
        private readonly IConfiguration configuration;
        private readonly ILoggerService loggerService;

        public RssStoreService(IConfiguration configuration, ILoggerService loggerService)
        {
            this.configuration = configuration;
            this.loggerService = loggerService;
        }

        public async Task<bool> SaveRssDataAsync(RssData rssData)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var dAdaprer = new SqlDataAdapter(
                    "select * from RSS", configuration.GetConnectionString("RssDB")))
                    using (new SqlCommandBuilder(dAdaprer))
                    {
                        var dataTable = new DataTable();
                        dAdaprer.Fill(dataTable);
                        var row = dataTable.NewRow();
                        row["Name"] = rssData.Name;
                        row["Username"] = rssData.Username;
                        row["Url"] = rssData.Url;
                        dataTable.Rows.Add(row);
                        dAdaprer.Update(dataTable);
                    }
                });
                return true;
            }
            catch (Exception e)
            {
                loggerService.Log($"SaveRssDataAsyncException message: {e.Message}");

                return false;
            }    
        }

        public async Task<IEnumerable<RssData>> LoadRssDataAsync()
        {
            try
            {
                var result = new List<RssData>();
                await Task.Run(() =>
                {
                    using (var dAdaprer = new SqlDataAdapter(
                    "select * from RSS", configuration.GetConnectionString("RssDB")))
                    {
                        var dataTable = new DataTable();
                        dAdaprer.Fill(dataTable);
                        foreach (var row in dataTable.Rows)
                        {
                            var rssData = new RssData
                            {
                                Name = ((DataRow)row)["Name"].ToString(),
                                Username = ((DataRow)row)["Username"].ToString(),
                                Url = ((DataRow)row)["Url"].ToString(),
                            };
                            result.Add(rssData);
                        }
                    }
                });

                return result;
            }
            catch (Exception e)
            {
                loggerService.Log($"LoadRssDataAsyncException message: {e.Message}");
                return default;
            }
        }
    }
}
