using System.Collections.Generic;

namespace HomeAutomata.ViewModels.Home
{
    public class HeatPumpTempsVM
    {
        private IList<HeatPumpTempVM> _heatPumpTemps;

        public IList<HeatPumpTempVM> HeatPumpTemps
        {
            get { return _heatPumpTemps ?? (_heatPumpTemps = new List<HeatPumpTempVM>()); }
            set { _heatPumpTemps = value; }
        }
    }
}