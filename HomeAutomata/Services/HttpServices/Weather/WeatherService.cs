using HomeAutomata.Services.HttpServices.Weather.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeAutomata.Services.HttpServices.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;

        public WeatherService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<CurrentWeather> GetCurrentWeather()
        {
            var url = "http://api.openweathermap.org/data/2.5/weather?lat=40.27&lon=22.5&appid=0026aabe6d4c7d37ef7a15ce5fdb63c7&units=metric&lang=el";
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CurrentWeather>(content);
            }

            return null;
        }
    }
}