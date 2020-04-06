using Newtonsoft.Json;
using System;

namespace RSSSender.Models
{
    public class RssData
    {
        [JsonProperty(PropertyName = "ID")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "Url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
    }
}
