using HomeAutomata.Core.Domain.HeatPumpModels;
using HomeAutomata.Core.Domain.Weather;
using HomeAutomata.Services.HeatPump;
using HomeAutomata.Services.HttpServices.Weather;
using HomeAutomata.Services.Weather;
using HomeAutomata.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace HomeAutomata.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weather;
        private readonly IOutsideWeatherService _service;
        private readonly IHeatPumpService _pumpService;

        public HomeController(IWeatherService weatherService,
                              IOutsideWeatherService outside,
                              IHeatPumpService heatPumpService)
        {
            _weather = weatherService;
            _service = outside;
            _pumpService = heatPumpService;
        }

        private void PrepareHeatPumpTempsModel(HeatPumpTempsVM model, IEnumerable<HeatPumpTemp> temps)
        {
            if (temps?.Any() == true)
            {
                foreach (var temp in temps)
                {
                    var item = new HeatPumpTempVM
                    {
                        Date = temp.Date,
                        Id = temp.Id,
                        Kwh = temp.Kwh
                    };

                    model.HeatPumpTemps.Add(item);
                }
            }
        }

        private void PrepareHeatPumpTempModel(HeatPumpTempVM model, HeatPumpTemp pumpTemp)
        {
            if (pumpTemp != null)
            {
                model.Date = pumpTemp.Date;
                model.Id = pumpTemp.Id;
                model.Kwh = pumpTemp.Kwh;
            }
        }

        public IActionResult Index()
        {
            var data = _service.GetAll();

            return View(data);
        }

        public IActionResult HeatPump()
        {
            var model = new HeatPumpTempsVM();
            var data = _pumpService.GetAll();
            PrepareHeatPumpTempsModel(model, data);
            return View(model);
        }

        [HttpGet]
        public IActionResult AddPumpData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPumpData(HeatPumpTempVM model)
        {
            if (ModelState.IsValid)
            {
                var data = new HeatPumpTemp
                {
                    Date = DateTime.Now,
                    Kwh = model.Kwh
                };

                var result = _pumpService.Add(data);

                return RedirectToAction(nameof(HeatPump));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult UpdatePumpData(int id)
        {
            var data = _pumpService.Get(id);
            var model = new HeatPumpTempVM();
            PrepareHeatPumpTempModel(model, data);
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdatePumpData(HeatPumpTempVM model)
        {
            var data = _pumpService.Get(model.Id);
            if (ModelState.IsValid)
            {
                data.Kwh = model.Kwh;

                var result = _pumpService.Update(data);

                return RedirectToAction(nameof(HeatPump));
            }

            PrepareHeatPumpTempModel(model, data);
            return View(model);
        }
    }
}