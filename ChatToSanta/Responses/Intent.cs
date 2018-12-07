using Newtonsoft.Json;

namespace ChatToSanta.Responses
{
    public class Intent
    {
        [JsonProperty(PropertyName = "intent")]
        public string IntentString { get; set; }

        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }
    }
}