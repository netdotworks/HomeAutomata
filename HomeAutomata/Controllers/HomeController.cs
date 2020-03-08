using HomeAutomata.Services.HttpServices.Weather;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomata.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weather;

        public HomeController(IWeatherService weatherService)
        {
            _weather = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}