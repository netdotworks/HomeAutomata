using HomeAutomata.Core.Domain.HeatPumpModels;
using HomeAutomata.Data.Repositories;
using HomeAutomata.Data.Services;

namespace HomeAutomata.Services.HeatPump
{
    public class HeatPumpService : CrudService<HeatPumpTemp>, IHeatPumpService
    {
        public HeatPumpService(IRepo<HeatPumpTemp> repo) : base(repo)
        {
        }
    }
}