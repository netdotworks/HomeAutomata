using HomeAutomata.Core.Domain.HeatPumpModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeAutomata.Data.Mapping
{
    public class HeatPumpTempMap : EntityTypeConfiguration<HeatPumpTemp>
    {
        public override void Configure(EntityTypeBuilder<HeatPumpTemp> builder)
        {
            base.Configure(builder);
        }
    }
}