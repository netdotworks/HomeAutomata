using System.Collections.Generic;

namespace HomeAutomata.ViewModels.HeatPump
{
    public class HeatPumpConsumptionsVM
    {
        private IList<HeatPumpConsumptionVM> _consumptions;

        public IList<HeatPumpConsumptionVM> Consumptions
        {
            get { return _consumptions ?? (_consumptions = new List<HeatPumpConsumptionVM>()); }
            set { _consumptions = value; }
        }
    }
}