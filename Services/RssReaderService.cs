using System;
using WilderMinds.RssSyndication;

namespace RSSSender.Services
{
    public class RssReaderService : IRssReaderService
    {
        private readonly IRssBodyService rssBodyService;

        public RssReaderService(IRssBodyService rssBodyService)
            => this.rssBodyService = rssBodyService;

        public string GetRss(string url)
        {
            var feed = new Feed { Link = new Uri(url) };

            var body = rssBodyService.GetBody(@$"{Environment.CurrentDirectory}\tvnRssBody.txt");
            var item = new Item
            {
                Title = "Najnowsze",
                Body = body,
                Link = new Uri("https://tvn24.pl/najnowsze.xml"),
                PublishDate = DateTime.UtcNow,
                Author = new Author { Name = "Jacek", Email = "jacek@tvn.pl" }
            };

            feed.Items.Add(item);
            return feed.Serialize();
        }
    }
}
