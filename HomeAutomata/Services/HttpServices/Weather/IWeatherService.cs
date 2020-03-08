using HomeAutomata.Services.HttpServices.Weather.Models;
using System.Threading.Tasks;

namespace HomeAutomata.Services.HttpServices.Weather
{
    public interface IWeatherService
    {
        Task<CurrentWeather> GetCurrentWeather();
    }
}