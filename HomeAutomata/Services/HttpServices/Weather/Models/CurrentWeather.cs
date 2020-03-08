using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomeAutomata.Services.HttpServices.Weather.Models
{
    public class CurrentWeather
    {
        [JsonProperty("weather")]
        public IList<Weather> Weather { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("sys")]
        public Sys Sys { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}