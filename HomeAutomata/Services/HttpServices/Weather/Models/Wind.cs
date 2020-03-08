using Newtonsoft.Json;

namespace HomeAutomata.Services.HttpServices.Weather.Models
{
    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public long Deg { get; set; }
    }
}