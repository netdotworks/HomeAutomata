using Newtonsoft.Json;

namespace HomeAutomata.Services.HttpServices.Weather.Models
{
    public partial class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}