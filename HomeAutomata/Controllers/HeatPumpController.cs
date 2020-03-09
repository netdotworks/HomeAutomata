using HomeAutomata.Core.Domain.HeatPumpModels;
using HomeAutomata.Services.HeatPump;
using HomeAutomata.ViewModels.HeatPump;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeAutomata.Controllers
{
    public class HeatPumpController : Controller
    {
        private readonly IHeatPumpConsumptionService _consumption;

        public HeatPumpController(IHeatPumpConsumptionService heatPumpConsumptionService)
        {
            _consumption = heatPumpConsumptionService;
        }

        private void PrepareHeatPumpConsumptionsModel(HeatPumpConsumptionsVM model, IEnumerable<HeatPumpConsumption> consumptions)
        {
            if (consumptions?.Any() == true)
            {
                foreach (var consumption in consumptions)
                {
                    var item = new HeatPumpConsumptionVM
                    {
                        Date = consumption.Date,
                        Id = consumption.Id,
                        Kwh = consumption.Kwh
                    };

                    model.Consumptions.Add(item);
                }
            }
        }

        private void PrepareHeatPumpConsumptionModel(HeatPumpConsumptionVM model, HeatPumpConsumption consumption)
        {
            if (consumption != null)
            {
                model.Date = consumption.Date;
                model.Id = consumption.Id;
                model.Kwh = consumption.Kwh;
            }
        }

        [HttpGet]
        public IActionResult HeatPumpConsumptions()
        {
            var model = new HeatPumpConsumptionsVM();
            var consumptions = _consumption.GetAll();

            PrepareHeatPumpConsumptionsModel(model, consumptions.OrderByDescending(o => o.Date));
            return View(model);
        }

        [HttpGet]
        public IActionResult AddConsumption()
        {
            var model = new HeatPumpConsumptionVM();
            PrepareHeatPumpConsumptionModel(model, null);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddConsumption(HeatPumpConsumptionVM model)
        {
            if (ModelState.IsValid)
            {
                var consumption = new HeatPumpConsumption
                {
                    Date = DateTime.Now,
                    Kwh = model.Kwh
                };

                var result = _consumption.Add(consumption);

                return RedirectToAction(nameof(HeatPumpConsumptions));
            }
            PrepareHeatPumpConsumptionModel(model, null);
            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateConsumption(int id)
        {
            var consumption = _consumption.Get(id);
            var model = new HeatPumpConsumptionVM();
            PrepareHeatPumpConsumptionModel(model, consumption);
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateConsumption(HeatPumpConsumptionVM model)
        {
            var consumption = _consumption.Get(model.Id);
            if (ModelState.IsValid)
            {
                consumption.Kwh = model.Kwh;

                var result = _consumption.Update(consumption);

                return RedirectToAction(nameof(HeatPumpConsumptions));
            }

            PrepareHeatPumpConsumptionModel(model, consumption);
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteConsumption(int id)
        {
            var consumption = _consumption.Get(id);
            var result = _consumption.Delete(consumption);
            return RedirectToAction(nameof(HeatPumpConsumptions));
        }
    }
}