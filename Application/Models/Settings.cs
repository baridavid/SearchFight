using Newtonsoft.Json;

namespace Application.Models
{
    [JsonObject("ConnectionStrings")]
    public class Settings
    {
        [JsonProperty("GoogleURL")]
        public string GoogleURL { get; set; }

        [JsonProperty("BingURL")]
        public string BingURL { get; set; }

        [JsonProperty("GoogleAPIKey")]
        public string GoogleAPIKey { get; set; }

        [JsonProperty("GoogleCEKey")]
        public string GoogleCEKey { get; set; }

        [JsonProperty("BingKey")]
        public string BingKey { get; set; }

        [JsonProperty("BingCustomConfig")]
        public string BingCustomConfig { get; set; }
    }
}
