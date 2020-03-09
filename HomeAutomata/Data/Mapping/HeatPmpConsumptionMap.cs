using HomeAutomata.Core.Domain.HeatPumpModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeAutomata.Data.Mapping
{
    public class HeatPmpConsumptionMap : EntityTypeConfiguration<HeatPumpConsumption>
    {
        public override void Configure(EntityTypeBuilder<HeatPumpConsumption> builder)
        {
            base.Configure(builder);
        }
    }
}