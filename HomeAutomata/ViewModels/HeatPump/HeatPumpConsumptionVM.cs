using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomata.ViewModels.HeatPump
{
    public class HeatPumpConsumptionVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Kwh { get; set; }
    }
}