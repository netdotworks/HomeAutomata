using System;

namespace HomeAutomata.Core.Domain.Weather
{
    public class OutsideWeather : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }

        public long Humidity { get; set; }
    }
}