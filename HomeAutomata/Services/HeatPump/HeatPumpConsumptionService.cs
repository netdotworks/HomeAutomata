using HomeAutomata.Core.Domain.HeatPumpModels;
using HomeAutomata.Data.Repositories;
using HomeAutomata.Data.Services;

namespace HomeAutomata.Services.HeatPump
{
    public class HeatPumpConsumptionService : CrudService<HeatPumpConsumption>, IHeatPumpConsumptionService
    {
        public HeatPumpConsumptionService(IRepo<HeatPumpConsumption> repo) : base(repo)
        {
        }
    }
}