using HomeAutomata.Core.Domain.Weather;
using HomeAutomata.Services.HttpServices.Weather;
using HomeAutomata.Services.Weather;
using System;

namespace HomeAutomata.Hangfire.RecurringJobs
{
    public class LogOutsideWeatherJob : ILogOutsideWeatherJob
    {
        private readonly IOutsideWeatherService _outsideWeatherService;
        private readonly IWeatherService _weatherService;

        public LogOutsideWeatherJob(IOutsideWeatherService outsideWeatherService, IWeatherService weatherService)
        {
            _outsideWeatherService = outsideWeatherService;
            _weatherService = weatherService;
        }

        public void LogWeather()
        {
            var weather = _weatherService.GetCurrentWeather().Result;
            var data = new OutsideWeather
            {
                FeelsLike = weather.Main.FeelsLike,
                Humidity = weather.Main.Humidity,
                Temperature = weather.Main.Temp,
                Date = DateTime.Now
            };

            _outsideWeatherService.Add(data);
        }
    }
}