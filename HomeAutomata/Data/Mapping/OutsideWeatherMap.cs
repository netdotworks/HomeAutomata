using HomeAutomata.Core.Domain.Weather;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeAutomata.Data.Mapping
{
    public class OutsideWeatherMap : EntityTypeConfiguration<OutsideWeather>
    {
        public override void Configure(EntityTypeBuilder<OutsideWeather> builder)
        {
            base.Configure(builder);
        }
    }
}