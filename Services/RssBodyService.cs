using System.IO;

namespace RSSSender.Services
{
    public class RssBodyService : IRssBodyService
    {
        public string GetBody(string path)
        {
            using (var reader = File.OpenText(path))
                return reader.ReadToEnd();
        }
    }
}
