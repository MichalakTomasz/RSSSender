using System.Collections.Generic;

namespace RSSSender.ViewModels
{
    public class RssResponseViewModel
    {
        public string RssBody { get; set; }
        public IEnumerable<string> Links { get; set; }
    }
}
