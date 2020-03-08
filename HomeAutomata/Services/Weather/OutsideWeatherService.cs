using HomeAutomata.Core.Domain.Weather;
using HomeAutomata.Data.Repositories;
using HomeAutomata.Data.Services;

namespace HomeAutomata.Services.Weather
{
    public class OutsideWeatherService : CrudService<OutsideWeather>, IOutsideWeatherService
    {
        public OutsideWeatherService(IRepo<OutsideWeather> repo) : base(repo)
        {
        }
    }
}