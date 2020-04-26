using System.Collections.Generic;

namespace RSSSender.ViewModels
{
    public class RssResponseViewModel
    {
        public string Body { get; set; }
        public IEnumerable<string> Links { get; set; }
    }
}
